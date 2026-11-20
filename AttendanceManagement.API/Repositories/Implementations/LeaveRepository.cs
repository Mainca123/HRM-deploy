using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Leave;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class LeaveRepository : ILeaveRepository
{
    private readonly AppDbContext _context;

    public LeaveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Absence>> GetAbsenceTypesAsync()
    {
        return await _context.Absences.AsNoTracking()
            .OrderBy(a => a.Id)
            .ToListAsync();
    }

    public async Task<bool> AbsenceExistsAsync(string absenceId)
    {
        return await _context.Absences.AnyAsync(a => a.Id == absenceId);
    }

    public async Task<PagedResultDto<LeaveRequestDto>> GetLeaveRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? status,
        DateTime? fromDate,
        DateTime? toDate)
    {
        IQueryable<RegisterAbsence> query = _context.RegisterAbsences
            .AsNoTracking()
            .Include(r => r.Employee)
            .Include(r => r.Absence);

        if (employeeId.HasValue)
        {
            query = query.Where(r => r.EmployeeId == employeeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            query = query.Where(r => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == r.EmployeeId && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        if (status.HasValue)
        {
            query = query.Where(r => r.Status == status.Value);
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
            .Select(r => new LeaveRequestDto
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
                Unit = r.Unit,
                AbsenceId = r.AbsenceId,
                AbsenceName = r.Absence.Name,
                Notes = r.Notes,
                Status = r.Status,
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate,
                ModifiedUserName = r.ModifiedUserName,
                ModifiedDate = r.ModifiedDate,
                ApprovedUserName = r.ApprovedUserName,
                ApprovedDate = r.ApprovedDate
            })
            .ToListAsync();

        return new PagedResultDto<LeaveRequestDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<LeaveRequestDto?> GetLeaveRequestDetailAsync(int employeeId, DateTime fromDate, string absenceId)
    {
        return await _context.RegisterAbsences
            .AsNoTracking()
            .Include(r => r.Employee)
            .Include(r => r.Absence)
            .Where(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.AbsenceId == absenceId)
            .Select(r => new LeaveRequestDto
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
                Unit = r.Unit,
                AbsenceId = r.AbsenceId,
                AbsenceName = r.Absence.Name,
                Notes = r.Notes,
                Status = r.Status,
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate,
                ModifiedUserName = r.ModifiedUserName,
                ModifiedDate = r.ModifiedDate,
                ApprovedUserName = r.ApprovedUserName,
                ApprovedDate = r.ApprovedDate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<RegisterAbsence?> GetLeaveRequestEntityAsync(int employeeId, DateTime fromDate, string absenceId)
    {
        return await _context.RegisterAbsences
            .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.AbsenceId == absenceId);
    }

    public async Task<bool> HasOverlappingLeaveAsync(
        int employeeId,
        DateTime fromDate,
        DateTime toDate,
        DateTime? excludeFromDate = null,
        string? excludeAbsenceId = null)
    {
        var query = _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => r.EmployeeId == employeeId && (r.Status == 0 || r.Status == 1));

        if (excludeFromDate.HasValue && !string.IsNullOrEmpty(excludeAbsenceId))
        {
            query = query.Where(r => !(r.FromDate == excludeFromDate.Value && r.AbsenceId == excludeAbsenceId));
        }

        // Hai khoảng [A_start, A_end] và [B_start, B_end] giao nhau khi A_start <= B_end && B_start <= A_end
        return await query.AnyAsync(r =>
            r.FromDate <= toDate &&
            (r.ToDate ?? r.FromDate) >= fromDate);
    }

    public async Task CreateLeaveRequestAsync(RegisterAbsence entity)
    {
        _context.RegisterAbsences.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLeaveRequestAsync(RegisterAbsence entity)
    {
        _context.RegisterAbsences.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteLeaveRequestAsync(RegisterAbsence entity)
    {
        _context.RegisterAbsences.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<LeaveBalanceDto?> GetAnnualLeaveBalanceAsync(int employeeId, short year)
    {
        var employee = await _context.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee == null)
        {
            return null;
        }

        // Tổng phép được cấp trong năm (trong DB của thầy Month=0 lưu tổng phép năm, hoặc cộng dồn theo tháng)
        var totalAllocated = await _context.AnnualLeaves
            .AsNoTracking()
            .Where(a => a.EmployeeId == employeeId && a.Year == year)
            .SumAsync(a => a.Value ?? 0m);

        // Tính ngày nghỉ phép năm ("P") đã duyệt (Status == 1)
        var usedDays = await _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => r.EmployeeId == employeeId
                     && r.AbsenceId == "P"
                     && r.FromDate.Year == year
                     && r.Status == 1)
            .SumAsync(r => r.Unit ?? 0m);

        // Tính ngày nghỉ phép năm ("P") đang chờ duyệt (Status == 0)
        var pendingDays = await _context.RegisterAbsences
            .AsNoTracking()
            .Where(r => r.EmployeeId == employeeId
                     && r.AbsenceId == "P"
                     && r.FromDate.Year == year
                     && r.Status == 0)
            .SumAsync(r => r.Unit ?? 0m);

        return new LeaveBalanceDto
        {
            EmployeeId = employee.Id,
            EmployeeCode = employee.Code,
            EmployeeFullName = employee.FullName,
            Year = year,
            TotalAllocated = totalAllocated,
            UsedDays = usedDays,
            PendingDays = pendingDays
        };
    }
}

