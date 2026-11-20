using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly AppDbContext _context;

    public AttendanceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ShiftDto>> GetShiftsAsync()
    {
        var shifts = await _context.Shifts.AsNoTracking().ToListAsync();
        var shiftDetails = await _context.ShiftDetails.AsNoTracking().ToListAsync();

        return shifts.Select(s =>
        {
            var detail = shiftDetails
                .Where(d => d.ShiftId == s.Id)
                .OrderByDescending(d => d.FromDate)
                .FirstOrDefault();

            return new ShiftDto
            {
                Id = s.Id,
                Name = s.Name,
                IsDefault = s.IsDefault,
                IsNightShift = s.IsNightShift,
                NormalTimeIn = detail?.NormalTimeIn,
                NormalTimeOut = detail?.NormalTimeOut,
                NormalBreakOut = detail?.NormalBreakOut,
                NormalBreakIn = detail?.NormalBreakIn,
                NormalTotalMinutes = detail?.NormalTotal,
                LateComingMinutes = detail?.LateComing,
                EarlyReturningMinutes = detail?.EarlyReturning
            };
        }).OrderByDescending(s => s.IsDefault).ThenBy(s => s.Id).ToList();
    }

    public async Task<Shift?> GetShiftByIdAsync(string shiftId)
    {
        return await _context.Shifts.AsNoTracking().FirstOrDefaultAsync(s => s.Id == shiftId);
    }

    public async Task<Shift?> GetDefaultShiftAsync()
    {
        return await _context.Shifts.AsNoTracking().FirstOrDefaultAsync(s => s.IsDefault == true)
            ?? await _context.Shifts.AsNoTracking().FirstOrDefaultAsync(s => s.Id == "HC")
            ?? await _context.Shifts.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<PagedResultDto<TimeInTimeOutDto>> GetDailyAttendancePagedAsync(
        int pageIndex,
        int pageSize,
        DateTime date,
        string? departmentId,
        string? search)
    {
        var workingDay = date.Date;

        IQueryable<TimeInTimeOut> query = _context.TimeInTimeOuts
            .AsNoTracking()
            .Include(t => t.Employee)
            .Include(t => t.Shift)
            .Where(t => t.WorkingDay == workingDay);

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            query = query.Where(t => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == t.EmployeeId && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim().ToLower();
            query = query.Where(t =>
                t.Employee.FullName.ToLower().Contains(keyword) ||
                (t.Employee.Code != null && t.Employee.Code.ToLower().Contains(keyword)));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(t => t.EmployeeId)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TimeInTimeOutDto
            {
                EmployeeId = t.EmployeeId,
                EmployeeCode = t.Employee.Code,
                EmployeeFullName = t.Employee.FullName,
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == t.EmployeeId && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                WorkingDay = t.WorkingDay,
                ShiftId = t.ShiftId,
                ShiftName = t.Shift.Name,
                TimeIn = t.TimeIn,
                TimeOut = t.TimeOut,
                Status = t.Status,
                Notes = t.Notes,
                WorkingHours = t.TimeIn.HasValue && t.TimeOut.HasValue
                    ? Math.Round((t.TimeOut.Value - t.TimeIn.Value).TotalHours, 1)
                    : null
            })
            .ToListAsync();

        return new PagedResultDto<TimeInTimeOutDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<List<TimeInTimeOutDto>> GetMyAttendanceHistoryAsync(int employeeId, int month, int year)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        var records = await _context.TimeInTimeOuts
            .AsNoTracking()
            .Include(t => t.Employee)
            .Include(t => t.Shift)
            .Where(t => t.EmployeeId == employeeId && t.WorkingDay >= startDate && t.WorkingDay <= endDate)
            .OrderByDescending(t => t.WorkingDay)
            .Select(t => new TimeInTimeOutDto
            {
                EmployeeId = t.EmployeeId,
                EmployeeCode = t.Employee.Code,
                EmployeeFullName = t.Employee.FullName,
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == t.EmployeeId && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                WorkingDay = t.WorkingDay,
                ShiftId = t.ShiftId,
                ShiftName = t.Shift.Name,
                TimeIn = t.TimeIn,
                TimeOut = t.TimeOut,
                Status = t.Status,
                Notes = t.Notes,
                WorkingHours = t.TimeIn.HasValue && t.TimeOut.HasValue
                    ? Math.Round((t.TimeOut.Value - t.TimeIn.Value).TotalHours, 1)
                    : null
            })
            .ToListAsync();

        return records;
    }

    public async Task<TimeInTimeOut?> GetTodayTimeRecordAsync(int employeeId, DateTime workingDay, string? shiftId = null)
    {
        var day = workingDay.Date;
        var query = _context.TimeInTimeOuts.Where(t => t.EmployeeId == employeeId && t.WorkingDay == day);

        if (!string.IsNullOrEmpty(shiftId))
        {
            query = query.Where(t => t.ShiftId == shiftId);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateTimeInTimeOutAsync(TimeInTimeOut record)
    {
        _context.TimeInTimeOuts.Add(record);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTimeInTimeOutAsync(TimeInTimeOut record)
    {
        _context.TimeInTimeOuts.Update(record);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResultDto<RegisterLateEarlyDto>> GetLateEarlyRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        DateTime? fromDate,
        DateTime? toDate)
    {
        IQueryable<RegisterLateEarly> query = _context.RegisterLateEarlies
            .AsNoTracking()
            .Include(r => r.Employee);

        if (employeeId.HasValue)
        {
            query = query.Where(r => r.EmployeeId == employeeId.Value);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(r => r.FromDate >= fromDate.Value.Date);
        }

        if (toDate.HasValue)
        {
            var endOfDay = toDate.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(r => r.FromDate <= endOfDay);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(r => r.FromDate)
            .ThenByDescending(r => r.CreatedDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new RegisterLateEarlyDto
            {
                EmployeeId = r.EmployeeId,
                EmployeeCode = r.Employee.Code,
                EmployeeFullName = r.Employee.FullName,
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == r.EmployeeId && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                Type = r.Type,
                Value = r.Value,
                Reason = r.Reason,
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate,
                ModifiedUserName = r.ModifiedUserName,
                ModifiedDate = r.ModifiedDate
            })
            .ToListAsync();

        return new PagedResultDto<RegisterLateEarlyDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<RegisterLateEarly?> GetLateEarlyEntityAsync(int employeeId, DateTime fromDate, bool type)
    {
        return await _context.RegisterLateEarlies
            .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.Type == type);
    }

    public async Task CreateLateEarlyAsync(RegisterLateEarly entity)
    {
        _context.RegisterLateEarlies.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLateEarlyAsync(RegisterLateEarly entity)
    {
        _context.RegisterLateEarlies.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLateEarlyAsync(RegisterLateEarly entity)
    {
        _context.RegisterLateEarlies.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResultDto<MonthlyAttendanceSummaryDto>> GetMonthlyAttendanceSummaryPagedAsync(
        int pageIndex,
        int pageSize,
        int month,
        int year,
        string? departmentId,
        string? search)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddTicks(-1);

        // Lọc nhân viên thỏa mãn điều kiện
        IQueryable<Employee> empQuery = _context.Employees.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            empQuery = empQuery.Where(e => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == e.Id && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim().ToLower();
            empQuery = empQuery.Where(e =>
                e.FullName.ToLower().Contains(keyword) ||
                (e.Code != null && e.Code.ToLower().Contains(keyword)));
        }

        var totalCount = await empQuery.CountAsync();

        var employees = await empQuery
            .OrderBy(e => e.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
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

        // Đọc dữ liệu chi tiết từ MonthAttendance trong tháng cho các nhân viên này
        var attendanceData = await _context.MonthAttendances
            .AsNoTracking()
            .Where(m => empIds.Contains(m.EmployeeId) && m.WorkingDay >= startDate && m.WorkingDay <= endDate)
            .ToListAsync();

        var summaryItems = employees.Select(e =>
        {
            var empData = attendanceData.Where(m => m.EmployeeId == e.Id).ToList();

            return new MonthlyAttendanceSummaryDto
            {
                EmployeeId = e.Id,
                EmployeeCode = e.Code,
                EmployeeFullName = e.FullName,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.DepartmentName,
                Month = month,
                Year = year,
                TotalWorkDays = empData.Where(m => m.AttendanceItemId == "NC").Sum(m => m.Value ?? 0),
                TotalOvertimeNormal = empData.Where(m => m.AttendanceItemId == "OT1").Sum(m => m.Value ?? 0),
                TotalOvertimeNormalNight = empData.Where(m => m.AttendanceItemId == "OT2").Sum(m => m.Value ?? 0),
                TotalOvertimeWeekend = empData.Where(m => m.AttendanceItemId == "OT3").Sum(m => m.Value ?? 0),
                TotalOvertimeWeekendNight = empData.Where(m => m.AttendanceItemId == "OT4").Sum(m => m.Value ?? 0),
                TotalOvertimeHoliday = empData.Where(m => m.AttendanceItemId == "OT5").Sum(m => m.Value ?? 0),
                TotalOvertimeHolidayNight = empData.Where(m => m.AttendanceItemId == "OT6").Sum(m => m.Value ?? 0),
                TotalLateMinutes = empData.Where(m => m.AttendanceItemId == "DM").Sum(m => m.Value ?? 0),
                TotalEarlyMinutes = empData.Where(m => m.AttendanceItemId == "VS").Sum(m => m.Value ?? 0),
                TotalMealAllowance = empData.Where(m => m.AttendanceItemId == "TA").Sum(m => m.Value ?? 0)
            };
        }).ToList();

        return new PagedResultDto<MonthlyAttendanceSummaryDto>
        {
            Items = summaryItems,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }
}

