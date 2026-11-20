namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO thông tin tóm tắt của nhân viên (phục vụ hiển thị danh sách phân trang)
/// </summary>
public class EmployeeSummaryDto
{
    /// <summary>
    /// Khóa chính ID nhân viên (số nguyên)
    /// </summary>
    /// <example>101</example>
    public int Id { get; set; }

    /// <summary>
    /// Mã định danh nhân viên
    /// </summary>
    /// <example>NV00101</example>
    public string? Code { get; set; }

    /// <summary>
    /// Họ và tên đầy đủ
    /// </summary>
    /// <example>Nguyễn Văn An</example>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Giới tính (true: Nam, false: Nữ, null: Chưa xác định)
    /// </summary>
    /// <example>true</example>
    public bool? Sex { get; set; }

    /// <summary>
    /// Ngày sinh
    /// </summary>
    /// <example>1995-05-15</example>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Địa chỉ email
    /// </summary>
    /// <example>an.nguyen@company.com</example>
    public string? Email { get; set; }

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
    /// Cho biết nhân viên đã được cấp tài khoản đăng nhập hay chưa
    /// </summary>
    /// <example>true</example>
    public bool HasAccount { get; set; }
}
