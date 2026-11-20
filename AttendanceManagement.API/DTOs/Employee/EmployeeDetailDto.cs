namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO thông tin chi tiết đầy đủ của nhân viên
/// </summary>
public class EmployeeDetailDto
{
    /// <summary>
    /// Mã ID nhân viên
    /// </summary>
    /// <example>101</example>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh nhân viên
    /// </summary>
    /// <example>NV00101</example>
    public string? Code { get; set; }

    /// <summary>
    /// Họ và tên
    /// </summary>
    /// <example>Nguyễn Văn An</example>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Bí danh / Tên thường gọi
    /// </summary>
    /// <example>Andy</example>
    public string? Alias { get; set; }

    /// <summary>
    /// Ngày sinh
    /// </summary>
    /// <example>1995-05-15</example>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Giới tính (true: Nam, false: Nữ)
    /// </summary>
    /// <example>true</example>
    public bool? Sex { get; set; }

    /// <summary>
    /// Email liên lạc
    /// </summary>
    /// <example>an.nguyen@company.com</example>
    public string? Email { get; set; }

    /// <summary>
    /// Đường dẫn ảnh đại diện
    /// </summary>
    public string? Photo { get; set; }

    /// <summary>
    /// Ghi chú hồ sơ
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Mã phòng ban hiện tại
    /// </summary>
    /// <example>1</example>
    public string? DepartmentId { get; set; }

    /// <summary>
    /// Tên phòng ban hiện tại
    /// </summary>
    /// <example>Phòng Kỹ thuật và Công nghệ</example>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// Mã chức vụ hiện tại
    /// </summary>
    /// <example>NV</example>
    public string? PositionId { get; set; }

    /// <summary>
    /// Tên chức vụ hiện tại
    /// </summary>
    /// <example>Nhân viên</example>
    public string? PositionName { get; set; }

    /// <summary>
    /// Ngày bắt đầu công tác tại phòng ban hiện tại
    /// </summary>
    public DateTime? CurrentDepartmentFromDate { get; set; }

    /// <summary>
    /// Tên tài khoản hệ thống được gắn với nhân viên này (nếu có)
    /// </summary>
    /// <example>an.nv</example>
    public string? UserName { get; set; }
}
