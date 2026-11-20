using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu danh mục tổ chức (Phòng ban, Chức vụ, Nhóm quyền)
/// </summary>
public interface IOrganizationRepository
{
    /// <summary>
    /// Lấy danh sách toàn bộ phòng ban
    /// </summary>
    /// <param name="activeOnly">Nếu true thì chỉ lấy phòng ban đang hoạt động (Active = true)</param>
    Task<List<Department>> GetDepartmentsAsync(bool activeOnly = false);

    /// <summary>
    /// Kiểm tra mã phòng ban có tồn tại trong hệ thống không
    /// </summary>
    Task<bool> DepartmentExistsAsync(string id);

    /// <summary>
    /// Lấy danh sách toàn bộ chức vụ
    /// </summary>
    Task<List<Position>> GetPositionsAsync();

    /// <summary>
    /// Kiểm tra mã chức vụ có tồn tại không
    /// </summary>
    Task<bool> PositionExistsAsync(string id);

    /// <summary>
    /// Lấy danh sách các nhóm quyền người dùng
    /// </summary>
    Task<List<UserGroup>> GetUserGroupsAsync();

    /// <summary>
    /// Kiểm tra mã nhóm quyền có tồn tại không
    /// </summary>
    Task<bool> UserGroupExistsAsync(string id);
}

