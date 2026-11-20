namespace AttendanceManagement.API.DTOs.User;

/// <summary>
/// DTO thông tin tóm tắt tài khoản người dùng
/// </summary>
public class UserSummaryDto
{
    /// <summary>
    /// Tên đăng nhập (Username)
    /// </summary>
    /// <example>an.nguyen</example>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// Mã nhóm quyền
    /// </summary>
    /// <example>EMPLOYEE</example>
    public string? GroupId { get; set; }

    /// <summary>
    /// Tên nhóm quyền hiển thị
    /// </summary>
    /// <example>Nhân viên</example>
    public string? GroupName { get; set; }

    /// <summary>
    /// Trạng thái hoạt động (true: kích hoạt, false: bị khóa)
    /// </summary>
    /// <example>true</example>
    public bool? Permission { get; set; }

    /// <summary>
    /// Mã ID nhân viên liên kết (nếu có)
    /// </summary>
    /// <example>101</example>
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Mã nhân viên liên kết
    /// </summary>
    /// <example>NV00101</example>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// Họ tên nhân viên liên kết
    /// </summary>
    /// <example>Nguyễn Văn An</example>
    public string? EmployeeFullName { get; set; }
}

