using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.User;

/// <summary>
/// DTO yêu cầu cập nhật thông tin tài khoản người dùng
/// </summary>
public class UpdateUserRequestDto
{
    /// <summary>
    /// Mã nhóm quyền mới
    /// </summary>
    /// <example>MANAGER</example>
    [Required(ErrorMessage = "Vui lòng chọn nhóm quyền")]
    public string GroupId { get; set; } = null!;

    /// <summary>
    /// Trạng thái hoạt động (true: kích hoạt, false: khóa tài khoản)
    /// </summary>
    /// <example>true</example>
    public bool Permission { get; set; } = true;

    /// <summary>
    /// Mã ID nhân viên liên kết (truyền null nếu muốn gỡ liên kết)
    /// </summary>
    /// <example>101</example>
    public int? EmployeeId { get; set; }
}

