using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ quản lý tài khoản người dùng
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Lấy danh sách tài khoản người dùng có phân trang và lọc
    /// </summary>
    Task<ApiResponse<PagedResultDto<UserSummaryDto>>> GetUsersPagedAsync(
        int pageIndex, 
        int pageSize, 
        string? search, 
        string? groupId);

    /// <summary>
    /// Lấy chi tiết thông tin tài khoản người dùng
    /// </summary>
    Task<ApiResponse<UserDetailDto>> GetUserDetailByNameAsync(string username);

    /// <summary>
    /// Cập nhật nhóm quyền, trạng thái hoạt động và liên kết nhân viên
    /// </summary>
    Task<ApiResponse<UserDetailDto>> UpdateUserAsync(string username, UpdateUserRequestDto request);

    /// <summary>
    /// Đặt lại mật khẩu tài khoản (dành cho Admin)
    /// </summary>
    Task<ApiResponse<bool>> ResetPasswordAsync(string username, AdminResetPasswordDto request);

    /// <summary>
    /// Xóa tài khoản người dùng
    /// </summary>
    Task<ApiResponse<bool>> DeleteUserAsync(string username, string currentUsername);
}

