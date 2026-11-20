namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO thông tin vị trí / chức vụ
/// </summary>
public class PositionDto
{
    /// <summary>
    /// Mã chức vụ
    /// </summary>
    /// <example>NV</example>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Tên chức vụ tiếng Việt
    /// </summary>
    /// <example>Nhân viên</example>
    public string? Name { get; set; }

    /// <summary>
    /// Tên chức vụ tiếng Anh
    /// </summary>
    /// <example>Staff</example>
    public string? NameE { get; set; }
}

