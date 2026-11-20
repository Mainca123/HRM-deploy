using AttendanceManagement.API.Data;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Repositories.Implementations;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AppDbContext _context;

    public OrganizationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetDepartmentsAsync(bool activeOnly = false)
    {
        IQueryable<Department> query = _context.Departments.AsNoTracking();

        if (activeOnly)
        {
            query = query.Where(d => d.Active == true);
        }

        return await query.OrderBy(d => d.Id).ToListAsync();
    }

    public async Task<bool> DepartmentExistsAsync(string id)
    {
        return await _context.Departments.AnyAsync(d => d.Id == id);
    }

    public async Task<List<Position>> GetPositionsAsync()
    {
        return await _context.Positions.AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    public async Task<bool> PositionExistsAsync(string id)
    {
        return await _context.Positions.AnyAsync(p => p.Id == id);
    }

    public async Task<List<UserGroup>> GetUserGroupsAsync()
    {
        return await _context.UserGroups.AsNoTracking()
            .OrderBy(g => g.Id)
            .ToListAsync();
    }

    public async Task<bool> UserGroupExistsAsync(string id)
    {
        return await _context.UserGroups.AnyAsync(g => g.Id == id);
    }
}

