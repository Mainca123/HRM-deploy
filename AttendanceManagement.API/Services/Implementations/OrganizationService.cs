using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ApiResponse<List<DepartmentDto>>> GetDepartmentsAsync(bool activeOnly = false)
    {
        var departments = await _organizationRepository.GetDepartmentsAsync(activeOnly);

        var dtos = departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            ParentId = d.ParentId,
            Active = d.Active
        }).ToList();

        return ApiResponse<List<DepartmentDto>>.Ok(dtos, "Lấy danh mục phòng ban thành công.");
    }

    public async Task<ApiResponse<List<PositionDto>>> GetPositionsAsync()
    {
        var positions = await _organizationRepository.GetPositionsAsync();

        var dtos = positions.Select(p => new PositionDto
        {
            Id = p.Id,
            Name = p.Name,
            NameE = p.NameE
        }).ToList();

        return ApiResponse<List<PositionDto>>.Ok(dtos, "Lấy danh mục chức vụ thành công.");
    }

    public async Task<ApiResponse<List<UserGroupDto>>> GetUserGroupsAsync()
    {
        var groups = await _organizationRepository.GetUserGroupsAsync();

        var dtos = groups.Select(g => new UserGroupDto
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description
        }).ToList();

        return ApiResponse<List<UserGroupDto>>.Ok(dtos, "Lấy danh mục nhóm quyền thành công.");
    }
}

