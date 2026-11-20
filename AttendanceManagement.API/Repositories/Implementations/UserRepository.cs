using AttendanceManagement.API.Data;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByNameAsync(string username, bool includeGroup = false)
    {
        IQueryable<User> query = _context.Users;

        if (includeGroup)
        {
            query = query.Include(u => u.Group);
        }

        return await query.FirstOrDefaultAsync(u => u.Name == username);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Name == username);
    }

    public async Task<UserEmployee?> GetUserEmployeeAsync(string username)
    {
        return await _context.UserEmployees.FirstOrDefaultAsync(ue => ue.UserName == username);
    }

    public async Task<bool> EmployeeHasAccountAsync(int employeeId)
    {
        return await _context.UserEmployees.AnyAsync(ue => ue.EmployeeId == employeeId);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
    }

    public async Task CreateUserWithEmployeeAsync(User user, UserEmployee userEmployee)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            _context.UserEmployees.Add(userEmployee);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdatePasswordAsync(User user, string newHashedPassword)
    {
        user.Password = newHashedPassword;
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResultDto<UserSummaryDto>> GetUsersPagedAsync(
        int pageIndex,
        int pageSize,
        string? search,
        string? groupId)
    {
        IQueryable<User> query = _context.Users.AsNoTracking().Include(u => u.Group);

        // Lọc theo nhóm quyền
        if (!string.IsNullOrWhiteSpace(groupId))
        {
            query = query.Where(u => u.GroupId == groupId);
        }

        // Lọc theo từ khóa tìm kiếm (Username hoặc tên nhân viên liên kết)
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim().ToLower();
            query = query.Where(u =>
                u.Name.ToLower().Contains(keyword) ||
                _context.UserEmployees.Any(ue => ue.UserName == u.Name &&
                    _context.Employees.Any(e => e.Id == ue.EmployeeId && e.FullName.ToLower().Contains(keyword))));
        }

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderBy(u => u.Name)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new UserSummaryDto
            {
                UserName = u.Name,
                GroupId = u.GroupId,
                GroupName = u.Group != null ? u.Group.Name : null,
                Permission = u.Permission,
                EmployeeId = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => ue.EmployeeId)
                    .FirstOrDefault(),
                EmployeeCode = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Employees
                        .Where(e => e.Id == ue.EmployeeId)
                        .Select(e => e.Code)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                EmployeeFullName = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Employees
                        .Where(e => e.Id == ue.EmployeeId)
                        .Select(e => e.FullName)
                        .FirstOrDefault())
                    .FirstOrDefault()
            })
            .ToListAsync();

        return new PagedResultDto<UserSummaryDto>
        {
            Items = users,
            TotalCount = totalCount,
            PageIndex = pageIndex,
            PageSize = pageSize
        };
    }

    public async Task<UserDetailDto?> GetUserDetailByNameAsync(string username)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.Name == username)
            .Select(u => new UserDetailDto
            {
                UserName = u.Name,
                GroupId = u.GroupId,
                GroupName = u.Group != null ? u.Group.Name : null,
                Permission = u.Permission,
                EmployeeId = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => ue.EmployeeId)
                    .FirstOrDefault(),
                EmployeeCode = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Employees
                        .Where(e => e.Id == ue.EmployeeId)
                        .Select(e => e.Code)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                EmployeeFullName = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Employees
                        .Where(e => e.Id == ue.EmployeeId)
                        .Select(e => e.FullName)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                EmployeeEmail = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Employees
                        .Where(e => e.Id == ue.EmployeeId)
                        .Select(e => e.Email)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                DepartmentName = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.EmployeeDepartments
                        .Where(ed => ed.EmployeeId == ue.EmployeeId && ed.ToDate == null)
                        .OrderByDescending(ed => ed.FromDate)
                        .Select(ed => ed.Department != null ? ed.Department.Name : null)
                        .FirstOrDefault())
                    .FirstOrDefault(),
                PositionName = _context.UserEmployees
                    .Where(ue => ue.UserName == u.Name)
                    .Select(ue => _context.Positions
                        .Where(p => p.Id == _context.EmployeePositions
                            .Where(ep => ep.EmployeeId == ue.EmployeeId && ep.ToDate == null)
                            .OrderByDescending(ep => ep.FromDate)
                            .Select(ep => ep.PositionId)
                            .FirstOrDefault())
                        .Select(p => p.Name)
                        .FirstOrDefault())
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task UpdateUserWithEmployeeAsync(User user, int? newEmployeeId)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // 1. Cập nhật thông tin User
            _context.Users.Update(user);

            // 2. Cập nhật bảng UserEmployee
            var existingUserEmp = await _context.UserEmployees.FirstOrDefaultAsync(ue => ue.UserName == user.Name);
            if (newEmployeeId.HasValue)
            {
                if (existingUserEmp != null)
                {
                    existingUserEmp.EmployeeId = newEmployeeId.Value;
                }
                else
                {
                    _context.UserEmployees.Add(new UserEmployee
                    {
                        UserName = user.Name,
                        EmployeeId = newEmployeeId.Value
                    });
                }
            }
            else
            {
                // Gỡ liên kết nhân viên nếu truyền null
                if (existingUserEmp != null)
                {
                    _context.UserEmployees.Remove(existingUserEmp);
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

    public async Task DeleteUserAsync(string username)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var userEmp = await _context.UserEmployees.FirstOrDefaultAsync(ue => ue.UserName == username);
            if (userEmp != null)
            {
                _context.UserEmployees.Remove(userEmp);
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user != null)
            {
                _context.Users.Remove(user);
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
