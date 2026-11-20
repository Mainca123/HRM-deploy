namespace AttendanceManagement.API.DTOs.User;

/// <summary>
/// DTO thông tin nhóm quyền người dùng
/// </summary>
public class UserGroupDto
{
    /// <summary>
    /// Mã nhóm quyền (khóa chính)
    /// </summary>
    /// <example>ADMIN</example>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Tên nhóm quyền hiển thị
    /// </summary>
    /// <example>Quản trị hệ thống</example>
    public string? Name { get; set; }

    /// <summary>
    /// Mô tả chi tiết vai trò của nhóm
    /// </summary>
    public string? Description { get; set; }
}

