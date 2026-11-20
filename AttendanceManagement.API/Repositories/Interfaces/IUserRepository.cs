using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng Repository: Chịu trách nhiệm trực tiếp tương tác với Database cho User, UserEmployee và UserGroup
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Tìm người dùng theo Username (kèm thông tin nhóm quyền Group nếu cần)
    /// </summary>
    Task<User?> GetByNameAsync(string username, bool includeGroup = false);

    /// <summary>
    /// Kiểm tra xem Username đã tồn tại trong bảng User chưa
    /// </summary>
    Task<bool> ExistsByUsernameAsync(string username);

    /// <summary>
    /// Lấy bản ghi liên kết User - Employee theo Username
    /// </summary>
    Task<UserEmployee?> GetUserEmployeeAsync(string username);

    /// <summary>
    /// Kiểm tra xem nhân viên (EmployeeId) đã có tài khoản trong UserEmployee chưa
    /// </summary>
    Task<bool> EmployeeHasAccountAsync(int employeeId);

    /// <summary>
    /// Lấy thông tin hồ sơ nhân viên theo ID
    /// </summary>
    Task<Employee?> GetEmployeeByIdAsync(int employeeId);

    /// <summary>
    /// Tạo đồng thời tài khoản User và liên kết UserEmployee trong một Database Transaction
    /// </summary>
    Task CreateUserWithEmployeeAsync(User user, UserEmployee userEmployee);

    /// <summary>
    /// Cập nhật mật khẩu mới cho người dùng
    /// </summary>
    Task UpdatePasswordAsync(User user, string newHashedPassword);

    /// <summary>
    /// Lấy danh sách tài khoản người dùng có phân trang, tìm kiếm và lọc nhóm quyền
    /// </summary>
    Task<PagedResultDto<UserSummaryDto>> GetUsersPagedAsync(
        int pageIndex, 
        int pageSize, 
        string? search, 
        string? groupId);

    /// <summary>
    /// Lấy chi tiết tài khoản người dùng kèm thông tin nhân viên liên kết
    /// </summary>
    Task<UserDetailDto?> GetUserDetailByNameAsync(string username);

    /// <summary>
    /// Cập nhật thông tin tài khoản và cập nhật liên kết nhân viên trong UserEmployee
    /// </summary>
    Task UpdateUserWithEmployeeAsync(User user, int? newEmployeeId);

    /// <summary>
    /// Xóa tài khoản người dùng và gỡ liên kết UserEmployee
    /// </summary>
    Task DeleteUserAsync(string username);
}
