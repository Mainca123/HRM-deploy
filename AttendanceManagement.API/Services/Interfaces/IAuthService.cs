using AttendanceManagement.API.DTOs.Auth;
using AttendanceManagement.API.DTOs.Common;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện định nghĩa các dịch vụ xác thực, phân quyền và quản lý tài khoản người dùng
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Xử lý đăng nhập hệ thống: kiểm tra tài khoản, đối chiếu mật khẩu và sinh mã JWT
    /// </summary>
    /// <param name="request">Thông tin đăng nhập gồm Username và Password</param>
    /// <returns>Đối tượng ApiResponse chứa Token và thông tin nhân viên</returns>
    Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request);

    /// <summary>
    /// Lấy thông tin chi tiết của người dùng đang đăng nhập dựa theo Username trong Token
    /// </summary>
    /// <param name="username">Tên đăng nhập lấy từ Claim của Token</param>
    /// <returns>Hồ sơ người dùng kèm thông tin phòng ban, chức vụ</returns>
    Task<ApiResponse<UserProfileDto>> GetCurrentUserAsync(string username);

    /// <summary>
    /// Tạo tài khoản mới cho nhân viên (chỉ dành cho HR hoặc Admin)
    /// </summary>
    /// <param name="request">Thông tin cấp tài khoản: Username, Password, EmployeeId, GroupId</param>
    /// <returns>Thông tin tài khoản vừa tạo thành công</returns>
    Task<ApiResponse<UserProfileDto>> CreateUserAsync(CreateUserRequestDto request);

    /// <summary>
    /// Xử lý đổi mật khẩu cho tài khoản người dùng
    /// </summary>
    /// <param name="username">Tên đăng nhập của tài khoản</param>
    /// <param name="request">Mật khẩu cũ và mật khẩu mới</param>
    /// <returns>True nếu đổi thành công</returns>
    Task<ApiResponse<bool>> ChangePasswordAsync(string username, ChangePasswordDto request);
}
