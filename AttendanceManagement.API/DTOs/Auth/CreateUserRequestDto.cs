using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Auth;

/// <summary>
/// Dữ liệu yêu cầu cấp tài khoản mới cho nhân viên (Chỉ dành cho ADMIN hoặc HR)
/// </summary>
public class CreateUserRequestDto
{
    /// <summary>
    /// Tên đăng nhập mới (duy nhất, không được trùng lặp)
    /// </summary>
    /// <example>nhanvien_01</example>
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [MaxLength(50, ErrorMessage = "Tên đăng nhập tối đa 50 ký tự")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// Mật khẩu khởi tạo ban đầu cho nhân viên (sẽ được tự động hash bằng BCrypt)
    /// </summary>
    /// <example>MatKhau@123</example>
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [MinLength(6, ErrorMessage = "Mật khẩu phải từ 6 ký tự trở lên")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// Mã ID của nhân viên trong bảng Employee cần được cấp tài khoản
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "Mã nhân viên (EmployeeId) không được để trống")]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Mã nhóm quyền hạn gán cho tài khoản (VD: 1 hoặc 4)
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "Nhóm quyền (GroupId) không được để trống")]
    public string GroupId { get; set; } = null!;
}
