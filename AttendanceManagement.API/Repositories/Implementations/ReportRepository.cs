using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Dashboard;
using AttendanceManagement.API.DTOs.Leave;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class ReportRepository : IReportRepository
{
    private readonly AppDbContext _context;

    public ReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(int employeeId)
    {
        var now = DateTime.Now;
        var today = now.Date;
        var currentMonth = now.Month;
        var currentYear = now.Year;
        var startDate = new DateTime(currentYear, currentMonth, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        var employee = await _context.Employees
            .AsNoTracking()
            .Where(e => e.Id == employeeId)
            .Select(e => new
            {
                e.Id,
                e.FullName,
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                PositionName = _context.Positions
                    .Where(p => p.Id == _context.EmployeePositions
                        .Where(ep => ep.EmployeeId == e.Id && ep.ToDate == null)
                        .OrderByDescending(ep => ep.FromDate)
                        .Select(ep => ep.PositionId)
                        .FirstOrDefault())
                    .Select(p => p.Name)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();

        // Số ngày công và số phút đi muộn tháng này
        var monthData = await _context.MonthAttendances
            .AsNoTracking()
            .Where(m => m.EmployeeId == employeeId && m.WorkingDay >= startDate && m.WorkingDay <= endDate)
            .ToListAsync();

        var workDays = monthData.Where(m => m.AttendanceItemId == "NC").Sum(m => m.Value ?? 0);
        var lateMinutes = (int)monthData.Where(m => m.AttendanceItemId == "DM").Sum(m => m.Value ?? 0);

        // Tổng giờ tăng ca tháng này (từ bảng RegisterOvertime)
        var startOnly = DateOnly.FromDateTime(startDate);
        var endOnly = DateOnly.FromDateTime(endDate);
        var overtimeHours = await _context.RegisterOvertimes
            .AsNoTracking()
            .Where(o => o.EmployeeId == employeeId && o.FromDate >= startOnly && o.FromDate <= endOnly && o.Value > 0)
            .SumAsync(o => o.Value ?? 0);

        // Số ngày phép năm còn lại
        var totalLeaveAllocated = await _context.AnnualLeaves
            .AsNoTracking()
            .Where(a => a.EmployeeId == employeeId && a.Year == currentYear)
            .SumAsync(a => a.Value ?? 0);

        var leaveUsed = await _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => r.EmployeeId == employeeId && r.AbsenceId == "P" && r.FromDate.Year == currentYear && r.Status == 1)
            .SumAsync(r => r.Unit ?? 0);

        var leaveBalance = totalLeaveAllocated - leaveUsed;

        // Trạng thái quẹt thẻ hôm nay
        var todayRecord = await _context.TimeInTimeOuts
            .AsNoTracking()
            .Include(t => t.Shift)
            .Where(t => t.EmployeeId == employeeId && t.WorkingDay == today)
            .FirstOrDefaultAsync();

        // 5 đơn nghỉ gần nhất
        var recentLeaves = await _context.RegisterAbsences
            .AsNoTracking()
            .Include(r => r.Absence)
            .Where(r => r.EmployeeId == employeeId)
            .OrderByDescending(r => r.FromDate)
            .Take(5)
            .Select(r => new LeaveRequestDto
            {
                EmployeeId = r.EmployeeId,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                Unit = r.Unit,
                AbsenceId = r.AbsenceId,
                AbsenceName = r.Absence.Name,
                Notes = r.Notes,
                Status = r.Status,
                CreatedDate = r.CreatedDate,
                ApprovedUserName = r.ApprovedUserName,
                ApprovedDate = r.ApprovedDate
            })
            .ToListAsync();

        return new EmployeeDashboardDto
        {
            EmployeeId = employeeId,
            EmployeeFullName = employee?.FullName ?? string.Empty,
            DepartmentName = employee?.DepartmentName,
            PositionName = employee?.PositionName,
            CurrentMonth = currentMonth,
            CurrentYear = currentYear,
            WorkDaysThisMonth = workDays,
            OvertimeHoursThisMonth = overtimeHours,
            AnnualLeaveBalance = leaveBalance,
            LateMinutesThisMonth = lateMinutes,
            TodayTimeIn = todayRecord?.TimeIn,
            TodayTimeOut = todayRecord?.TimeOut,
            TodayShiftName = todayRecord?.Shift?.Name,
            RecentLeaveRequests = recentLeaves
        };
    }

    public async Task<ManagerDashboardDto> GetManagerDashboardAsync(string? departmentId)
    {
        var today = DateTime.Today;

        IQueryable<Employee> empQuery = _context.Employees.AsNoTracking();
        string? deptName = null;

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            empQuery = empQuery.Where(e => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == e.Id && ed.DepartmentId == departmentId && ed.ToDate == null));

            deptName = await _context.Departments
                .Where(d => d.Id == departmentId)
                .Select(d => d.Name)
                .FirstOrDefaultAsync();
        }

        var totalDeptEmployees = await empQuery.CountAsync();
        var deptEmpIds = await empQuery.Select(e => e.Id).ToListAsync();

        // Quân số đi làm hôm nay (đã có bản ghi quẹt thẻ TimeIn trong ngày)
        var presentToday = await _context.TimeInTimeOuts
            .AsNoTracking()
            .Where(t => deptEmpIds.Contains(t.EmployeeId) && t.WorkingDay == today && t.TimeIn != null)
            .Select(t => t.EmployeeId)
            .Distinct()
            .CountAsync();

        var absentToday = Math.Max(0, totalDeptEmployees - presentToday);

        // Đơn nghỉ phép chờ duyệt
        var pendingLeaves = await _context.RegisterAbsences
            .AsNoTracking()
            .Include(r => r.Employee)
            .Include(r => r.Absence)
            .Where(r => deptEmpIds.Contains(r.EmployeeId) && r.Status == 0)
            .OrderByDescending(r => r.FromDate)
            .Take(10)
            .Select(r => new LeaveRequestDto
            {
                EmployeeId = r.EmployeeId,
                EmployeeCode = r.Employee.Code,
                EmployeeFullName = r.Employee.FullName,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                Unit = r.Unit,
                AbsenceId = r.AbsenceId,
                AbsenceName = r.Absence.Name,
                Notes = r.Notes,
                Status = r.Status,
                CreatedDate = r.CreatedDate
            })
            .ToListAsync();

        // Đơn giải trình đi muộn / về sớm chờ duyệt
        var pendingLateEarlies = await _context.RegisterLateEarlies
            .AsNoTracking()
            .Include(r => r.Employee)
            .Where(r => deptEmpIds.Contains(r.EmployeeId) && string.IsNullOrEmpty(r.ModifiedUserName))
            .OrderByDescending(r => r.FromDate)
            .Take(10)
            .Select(r => new RegisterLateEarlyDto
            {
                EmployeeId = r.EmployeeId,
                EmployeeCode = r.Employee.Code,
                EmployeeFullName = r.Employee.FullName,
                FromDate = r.FromDate,
                Type = r.Type,
                Value = r.Value,
                Reason = r.Reason,
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate
            })
            .ToListAsync();

        return new ManagerDashboardDto
        {
            DepartmentId = departmentId,
            DepartmentName = deptName,
            ReportDate = today,
            TotalDepartmentEmployees = totalDeptEmployees,
            PresentToday = presentToday,
            AbsentToday = absentToday,
            PendingLeaveRequestsCount = pendingLeaves.Count,
            PendingLateEarlyRequestsCount = pendingLateEarlies.Count,
            PendingLeaves = pendingLeaves,
            PendingLateEarlies = pendingLateEarlies
        };
    }

    public async Task<HrDashboardDto> GetHrDashboardAsync()
    {
        var now = DateTime.Now;
        var today = now.Date;
        var month = now.Month;
        var year = now.Year;
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        var totalActive = await _context.Employees.CountAsync();
        var totalDepts = await _context.Departments.Where(d => d.Active == true).CountAsync();

        // Quân số đi làm hôm nay trên toàn công ty
        var presentToday = await _context.TimeInTimeOuts
            .AsNoTracking()
            .Where(t => t.WorkingDay == today && t.TimeIn != null)
            .Select(t => t.EmployeeId)
            .Distinct()
            .CountAsync();

        var absentToday = Math.Max(0, totalActive - presentToday);

        // Nhân viên mới trong tháng
        var newEmployees = await _context.EmployeeDepartments
            .AsNoTracking()
            .Where(ed => ed.FromDate >= startDate && ed.FromDate <= endDate)
            .Select(ed => ed.EmployeeId)
            .Distinct()
            .CountAsync();

        // Tổng giờ OT toàn công ty tháng này
        var startOnly = DateOnly.FromDateTime(startDate);
        var endOnly = DateOnly.FromDateTime(endDate);
        var totalOtHours = await _context.RegisterOvertimes
            .AsNoTracking()
            .Where(o => o.FromDate >= startOnly && o.FromDate <= endOnly && o.Value > 0)
            .SumAsync(o => o.Value ?? 0);

        // Tổng số ngày nghỉ đã duyệt tháng này
        var totalLeaveDays = await _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => r.FromDate >= startDate && r.FromDate <= endDate && r.Status == 1)
            .SumAsync(r => r.Unit ?? 0);

        // Thống kê chi tiết theo từng phòng ban
        var departments = await _context.Departments
            .AsNoTracking()
            .Where(d => d.Active == true)
            .OrderBy(d => d.Id)
            .ToListAsync();

        var activeEmployeesWithDept = await _context.EmployeeDepartments
            .AsNoTracking()
            .Where(ed => ed.ToDate == null)
            .ToListAsync();

        var todayPresentEmpIds = await _context.TimeInTimeOuts
            .AsNoTracking()
            .Where(t => t.WorkingDay == today && t.TimeIn != null)
            .Select(t => t.EmployeeId)
            .Distinct()
            .ToListAsync();

        var deptStats = departments.Select(d =>
        {
            var deptEmpIds = activeEmployeesWithDept.Where(ed => ed.DepartmentId == d.Id).Select(ed => ed.EmployeeId).ToList();
            var deptTotal = deptEmpIds.Count;
            var deptPresent = deptEmpIds.Count(id => todayPresentEmpIds.Contains(id));

            return new DepartmentAttendanceStatDto
            {
                DepartmentId = d.Id,
                DepartmentName = d.Name ?? d.Id,
                TotalEmployees = deptTotal,
                PresentToday = deptPresent
            };
        }).ToList();

        return new HrDashboardDto
        {
            ReportDate = today,
            TotalActiveEmployees = totalActive,
            TotalDepartments = totalDepts,
            PresentToday = presentToday,
            AbsentToday = absentToday,
            Month = month,
            Year = year,
            TotalNewEmployeesThisMonth = newEmployees,
            TotalOvertimeHoursThisMonth = totalOtHours,
            TotalLeaveDaysThisMonth = totalLeaveDays,
            DepartmentStats = deptStats
        };
    }

    public async Task<List<LeaveSummaryReportDto>> GetLeaveSummaryReportAsync(DateTime fromDate, DateTime toDate, string? departmentId)
    {
        var start = fromDate.Date;
        var end = toDate.Date.AddDays(1).AddTicks(-1);

        IQueryable<Employee> empQuery = _context.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            empQuery = empQuery.Where(e => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == e.Id && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        var employees = await empQuery
            .OrderBy(e => e.Id)
            .Select(e => new
            {
                e.Id,
                e.Code,
                e.FullName,
                DepartmentId = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.DepartmentId)
                    .FirstOrDefault(),
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault()
            })
            .ToListAsync();

        var empIds = employees.Select(e => e.Id).ToList();

        var leaveRecords = await _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => empIds.Contains(r.EmployeeId) && r.FromDate >= start && r.FromDate <= end && r.Status == 1)
            .ToListAsync();

        return employees.Select(e =>
        {
            var empLeaves = leaveRecords.Where(r => r.EmployeeId == e.Id).ToList();

            return new LeaveSummaryReportDto
            {
                EmployeeId = e.Id,
                EmployeeCode = e.Code,
                EmployeeFullName = e.FullName,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName,
                AnnualLeaveDays = empLeaves.Where(r => r.AbsenceId == "P").Sum(r => r.Unit ?? 0),
                SickLeaveDays = empLeaves.Where(r => r.AbsenceId == "O").Sum(r => r.Unit ?? 0),
                UnpaidLeaveDays = empLeaves.Where(r => r.AbsenceId == "KL" || r.AbsenceId == "KLD" || r.AbsenceId == "KLCC").Sum(r => r.Unit ?? 0),
                MaternityLeaveDays = empLeaves.Where(r => r.AbsenceId == "NS" || r.AbsenceId == "TS" || r.AbsenceId == "DS").Sum(r => r.Unit ?? 0),
                OtherLeaveDays = empLeaves.Where(r => !new[] { "P", "O", "KL", "KLD", "KLCC", "NS", "TS", "DS" }.Contains(r.AbsenceId)).Sum(r => r.Unit ?? 0)
            };
        }).Where(r => r.TotalLeaveDays > 0).ToList();
    }

    public async Task<List<OvertimeSummaryReportDto>> GetOvertimeSummaryReportAsync(int month, int year, string? departmentId)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        IQueryable<Employee> empQuery = _context.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            empQuery = empQuery.Where(e => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == e.Id && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        var employees = await empQuery
            .OrderBy(e => e.Id)
            .Select(e => new
            {
                e.Id,
                e.Code,
                e.FullName,
                DepartmentId = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.DepartmentId)
                    .FirstOrDefault(),
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault()
            })
            .ToListAsync();

        var empIds = employees.Select(e => e.Id).ToList();

        // Đọc từ bảng MonthAttendance với các chỉ số OT1, OT2, OT3, OT4, OT5, OT6
        var otAttendanceData = await _context.MonthAttendances
            .AsNoTracking()
            .Where(m => empIds.Contains(m.EmployeeId)
                     && m.WorkingDay >= startDate
                     && m.WorkingDay <= endDate
                     && (m.AttendanceItemId == "OT1"
                      || m.AttendanceItemId == "OT2"
                      || m.AttendanceItemId == "OT3"
                      || m.AttendanceItemId == "OT4"
                      || m.AttendanceItemId == "OT5"
                      || m.AttendanceItemId == "OT6"))
            .ToListAsync();

        return employees.Select(e =>
        {
            var empOt = otAttendanceData.Where(m => m.EmployeeId == e.Id).ToList();

            return new OvertimeSummaryReportDto
            {
                EmployeeId = e.Id,
                EmployeeCode = e.Code,
                EmployeeFullName = e.FullName,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName,
                NormalOvertimeHours = empOt.Where(m => m.AttendanceItemId == "OT1").Sum(m => m.Value ?? 0),
                NightOvertimeHours = empOt.Where(m => m.AttendanceItemId == "OT2").Sum(m => m.Value ?? 0),
                WeekendOvertimeHours = empOt.Where(m => m.AttendanceItemId == "OT3" || m.AttendanceItemId == "OT4").Sum(m => m.Value ?? 0),
                HolidayOvertimeHours = empOt.Where(m => m.AttendanceItemId == "OT5" || m.AttendanceItemId == "OT6").Sum(m => m.Value ?? 0)
            };
        }).Where(r => r.TotalOvertimeHours > 0).ToList();
    }
}

