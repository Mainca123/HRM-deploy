using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.DTOs.User;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ cho các danh mục tổ chức (Phòng ban, Chức vụ, Nhóm quyền)
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// Lấy danh mục phòng ban
    /// </summary>
    Task<ApiResponse<List<DepartmentDto>>> GetDepartmentsAsync(bool activeOnly = false);

    /// <summary>
    /// Lấy danh mục chức vụ
    /// </summary>
    Task<ApiResponse<List<PositionDto>>> GetPositionsAsync();

    /// <summary>
    /// Lấy danh mục nhóm quyền
    /// </summary>
    Task<ApiResponse<List<UserGroupDto>>> GetUserGroupsAsync();
}

