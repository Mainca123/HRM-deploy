using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Auth;

/// <summary>
/// Dữ liệu yêu cầu đăng nhập hệ thống
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Tên đăng nhập của tài khoản (tương ứng cột Name trong bảng User)
    /// </summary>
    /// <example>admin</example>
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Mật khẩu của tài khoản
    /// </summary>
    /// <example>Abc@12345</example>
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string Password { get; set; } = null!;
}
