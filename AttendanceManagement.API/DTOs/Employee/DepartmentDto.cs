namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO thông tin phòng ban
/// </summary>
public class DepartmentDto
{
    /// <summary>
    /// Mã phòng ban (varchar)
    /// </summary>
    /// <example>1</example>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Tên phòng ban
    /// </summary>
    /// <example>Phòng Kỹ thuật và Công nghệ</example>
    public string? Name { get; set; }

    /// <summary>
    /// Mã phòng ban cha (nếu có cấp bậc)
    /// </summary>
    /// <example>null</example>
    public string? ParentId { get; set; }

    /// <summary>
    /// Trạng thái hoạt động
    /// </summary>
    /// <example>true</example>
    public bool? Active { get; set; }
}
