using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.User;

/// <summary>
/// DTO yêu cầu Admin đặt lại mật khẩu cho tài khoản người dùng
/// </summary>
public class AdminResetPasswordDto
{
    /// <summary>
    /// Mật khẩu mới được chỉ định (mặc định nếu để trống là 123456)
    /// </summary>
    /// <example>123456</example>
    [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
    public string NewPassword { get; set; } = "123456";
}

