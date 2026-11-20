using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Auth;

/// <summary>
/// Dữ liệu yêu cầu đổi mật khẩu tài khoản
/// </summary>
public class ChangePasswordDto
{
    /// <summary>
    /// Mật khẩu hiện tại của bạn
    /// </summary>
    /// <example>MatKhauCu@123</example>
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu cũ")]
    public string CurrentPassword { get; set; } = null!;

    /// <summary>
    /// Mật khẩu mới mong muốn thay đổi (tối thiểu 6 ký tự)
    /// </summary>
    /// <example>MatKhauMoi@456</example>
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới")]
    [MinLength(6, ErrorMessage = "Mật khẩu mới phải từ 6 ký tự trở lên")]
    public string NewPassword { get; set; } = null!;
}
