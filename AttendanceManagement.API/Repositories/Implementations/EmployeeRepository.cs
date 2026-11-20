using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResultDto<EmployeeSummaryDto>> GetEmployeesPagedAsync(
        int pageIndex,
        int pageSize,
        string? search,
        string? departmentId)
    {
        IQueryable<Employee> query = _context.Employees.AsNoTracking();

        // 1. Lọc theo từ khóa tìm kiếm (Mã nhân viên hoặc Họ tên)
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim().ToLower();
            query = query.Where(e =>
                (e.Code != null && e.Code.ToLower().Contains(keyword)) ||
                e.FullName.ToLower().Contains(keyword));
        }

        // 2. Lọc theo phòng ban hiện tại
        if (!string.IsNullOrWhiteSpace(departmentId))
        {
            query = query.Where(e => _context.EmployeeDepartments
                .Any(ed => ed.EmployeeId == e.Id && ed.DepartmentId == departmentId && ed.ToDate == null));
        }

        var totalCount = await query.CountAsync();

        // 3. Phân trang và ánh xạ dữ liệu DTO
        var employees = await query
            .OrderBy(e => e.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EmployeeSummaryDto
            {
                Id = e.Id,
                Code = e.Code,
                FullName = e.FullName,
                Sex = e.Sex,
                BirthDate = e.BirthDate,
                Email = e.Email,
                DepartmentId = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.DepartmentId)
                    .FirstOrDefault(),
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                PositionId = _context.EmployeePositions
                    .Where(ep => ep.EmployeeId == e.Id && ep.ToDate == null)
                    .OrderByDescending(ep => ep.FromDate)
                    .Select(ep => ep.PositionId)
                    .FirstOrDefault(),
                PositionName = _context.Positions
                    .Where(p => p.Id == _context.EmployeePositions
                        .Where(ep => ep.EmployeeId == e.Id && ep.ToDate == null)
                        .OrderByDescending(ep => ep.FromDate)
                        .Select(ep => ep.PositionId)
                        .FirstOrDefault())
                    .Select(p => p.Name)
                    .FirstOrDefault(),
                HasAccount = _context.UserEmployees.Any(ue => ue.EmployeeId == e.Id)
            })
            .ToListAsync();

        return new PagedResultDto<EmployeeSummaryDto>
        {
            Items = employees,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<EmployeeDetailDto?> GetEmployeeDetailByIdAsync(int id)
    {
        return await _context.Employees
            .AsNoTracking()
            .Where(e => e.Id == id)
            .Select(e => new EmployeeDetailDto
            {
                Id = e.Id,
                Code = e.Code,
                FullName = e.FullName,
                Alias = e.Alias,
                BirthDate = e.BirthDate,
                Sex = e.Sex,
                Email = e.Email,
                Photo = e.Photo,
                Notes = e.Notes,
                DepartmentId = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.DepartmentId)
                    .FirstOrDefault(),
                DepartmentName = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => ed.Department != null ? ed.Department.Name : null)
                    .FirstOrDefault(),
                CurrentDepartmentFromDate = _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == e.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .Select(ed => (DateTime?)ed.FromDate)
                    .FirstOrDefault(),
                PositionId = _context.EmployeePositions
                    .Where(ep => ep.EmployeeId == e.Id && ep.ToDate == null)
                    .OrderByDescending(ep => ep.FromDate)
                    .Select(ep => ep.PositionId)
                    .FirstOrDefault(),
                PositionName = _context.Positions
                    .Where(p => p.Id == _context.EmployeePositions
                        .Where(ep => ep.EmployeeId == e.Id && ep.ToDate == null)
                        .OrderByDescending(ep => ep.FromDate)
                        .Select(ep => ep.PositionId)
                        .FirstOrDefault())
                    .Select(p => p.Name)
                    .FirstOrDefault(),
                UserName = _context.UserEmployees
                    .Where(ue => ue.EmployeeId == e.Id)
                    .Select(ue => ue.UserName)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<int> GetNextIdAsync()
    {
        var maxId = await _context.Employees.MaxAsync(e => (int?)e.Id);
        return (maxId ?? 0) + 1;
    }

    public async Task<bool> ExistsByCodeAsync(string code, int? excludeId = null)
    {
        var query = _context.Employees.AsQueryable();
        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }
        return await query.AnyAsync(e => e.Code == code);
    }

    public async Task CreateEmployeeWithOrgAsync(
        Employee employee,
        string departmentId,
        string positionId,
        DateTime joinDate)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Thêm bản ghi nhân viên
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            // 2. Thêm bản ghi phòng ban ban đầu
            var employeeDept = new EmployeeDepartment
            {
                EmployeeId = employee.Id,
                DepartmentId = departmentId,
                FromDate = joinDate,
                ToDate = null
            };
            _context.EmployeeDepartments.Add(employeeDept);

            // 3. Thêm bản ghi chức vụ ban đầu
            var employeePos = new EmployeePosition
            {
                EmployeeId = employee.Id,
                PositionId = positionId,
                FromDate = joinDate,
                ToDate = null
            };
            _context.EmployeePositions.Add(employeePos);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateEmployeeWithOrgAsync(
        Employee employee,
        string? newDepartmentId,
        string? newPositionId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var now = DateTime.Now;

            // 1. Cập nhật phòng ban nếu có thay đổi
            if (!string.IsNullOrWhiteSpace(newDepartmentId))
            {
                var currentDept = await _context.EmployeeDepartments
                    .Where(ed => ed.EmployeeId == employee.Id && ed.ToDate == null)
                    .OrderByDescending(ed => ed.FromDate)
                    .FirstOrDefaultAsync();

                if (currentDept == null || currentDept.DepartmentId != newDepartmentId)
                {
                    if (currentDept != null)
                    {
                        currentDept.ToDate = now;
                    }

                    _context.EmployeeDepartments.Add(new EmployeeDepartment
                    {
                        EmployeeId = employee.Id,
                        DepartmentId = newDepartmentId,
                        FromDate = now,
                        ToDate = null
                    });
                }
            }

            // 2. Cập nhật chức vụ nếu có thay đổi
            if (!string.IsNullOrWhiteSpace(newPositionId))
            {
                var currentPos = await _context.EmployeePositions
                    .Where(ep => ep.EmployeeId == employee.Id && ep.ToDate == null)
                    .OrderByDescending(ep => ep.FromDate)
                    .FirstOrDefaultAsync();

                if (currentPos == null || currentPos.PositionId != newPositionId)
                {
                    if (currentPos != null)
                    {
                        currentPos.ToDate = now;
                    }

                    _context.EmployeePositions.Add(new EmployeePosition
                    {
                        EmployeeId = employee.Id,
                        PositionId = newPositionId,
                        FromDate = now,
                        ToDate = null
                    });
                }
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> HasDependentDataAsync(int id)
    {
        // Kiểm tra xem nhân viên đã có dữ liệu phát sinh ở các bảng nghiệp vụ chưa
        var hasAttendance = await _context.TimeInTimeOuts.AnyAsync(t => t.EmployeeId == id);
        if (hasAttendance) return true;

        var hasMonthAttendance = await _context.MonthAttendances.AnyAsync(m => m.EmployeeId == id);
        if (hasMonthAttendance) return true;

        var hasMonthSalary = await _context.MonthSalaries.AnyAsync(s => s.EmployeeId == id);
        if (hasMonthSalary) return true;

        var hasAbsence = await _context.RegisterAbsences.AnyAsync(a => a.EmployeeId == id);
        if (hasAbsence) return true;

        var hasOvertime = await _context.RegisterOvertimes.AnyAsync(o => o.EmployeeId == id);
        if (hasOvertime) return true;

        return false;
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Xóa liên kết tài khoản nếu có
            var userEmployees = await _context.UserEmployees.Where(ue => ue.EmployeeId == id).ToListAsync();
            if (userEmployees.Count != 0)
            {
                _context.UserEmployees.RemoveRange(userEmployees);
            }

            // 2. Xóa lịch sử phòng ban
            var deptHistories = await _context.EmployeeDepartments.Where(ed => ed.EmployeeId == id).ToListAsync();
            if (deptHistories.Count != 0)
            {
                _context.EmployeeDepartments.RemoveRange(deptHistories);
            }

            // 3. Xóa lịch sử chức vụ
            var posHistories = await _context.EmployeePositions.Where(ep => ep.EmployeeId == id).ToListAsync();
            if (posHistories.Count != 0)
            {
                _context.EmployeePositions.RemoveRange(posHistories);
            }

            // 4. Xóa nhân viên
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

