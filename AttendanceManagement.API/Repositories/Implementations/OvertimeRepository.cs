using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Overtime;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class OvertimeRepository : IOvertimeRepository
{
    private readonly AppDbContext _context;

    public OvertimeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OvertimeLimit>> GetOvertimeLimitsAsync()
    {
        return await _context.OvertimeLimits
            .AsNoTracking()
            .OrderByDescending(l => l.FromDate)
            .ToListAsync();
    }

    public async Task<PagedResultDto<OvertimeRequestDto>> GetOvertimeRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? type,
        DateOnly? fromDate,
        DateOnly? toDate)
    {
        IQueryable<RegisterOvertime> query = _context.RegisterOvertimes
            .AsNoTracking()
            .Include(r => r.Employee);

        if (employeeId.HasValue)
        {
            query = query.Where(r => r.EmployeeId == employeeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            query = query.Where(r => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == r.EmployeeId && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        if (type.HasValue)
        {
            query = query.Where(r => r.Type == type.Value);
        }

        if (fromDate.HasValue)
        {
            query = query.Where(r => r.FromDate >= fromDate.Value);
        }

        if (toDate.HasValue)
        {
            query = query.Where(r => r.FromDate <= toDate.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(r => r.FromDate)
            .ThenByDescending(r => r.CreatedDate)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new OvertimeRequestDto
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
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate,
                ModifiedUserName = r.ModifiedUserName,
                ModifiedDate = r.ModifiedDate
            })
            .ToListAsync();

        return new PagedResultDto<OvertimeRequestDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<OvertimeRequestDto?> GetOvertimeRequestDetailAsync(int employeeId, DateOnly fromDate, byte type)
    {
        return await _context.RegisterOvertimes
            .AsNoTracking()
            .Include(r => r.Employee)
            .Where(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.Type == type)
            .Select(r => new OvertimeRequestDto
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
                CreatedUserName = r.CreatedUserName,
                CreatedDate = r.CreatedDate,
                ModifiedUserName = r.ModifiedUserName,
                ModifiedDate = r.ModifiedDate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<RegisterOvertime?> GetOvertimeRequestEntityAsync(int employeeId, DateOnly fromDate, byte type)
    {
        return await _context.RegisterOvertimes
            .FirstOrDefaultAsync(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.Type == type);
    }

    public async Task<bool> ExistsOvertimeRequestAsync(int employeeId, DateOnly fromDate, byte type)
    {
        return await _context.RegisterOvertimes
            .AnyAsync(r => r.EmployeeId == employeeId && r.FromDate == fromDate && r.Type == type);
    }

    public async Task CreateOvertimeRequestAsync(RegisterOvertime entity)
    {
        _context.RegisterOvertimes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateOvertimeRequestAsync(RegisterOvertime entity)
    {
        _context.RegisterOvertimes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteOvertimeRequestAsync(RegisterOvertime entity)
    {
        _context.RegisterOvertimes.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

