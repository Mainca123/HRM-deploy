using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO yêu cầu cập nhật thông tin nhân viên
/// </summary>
public class UpdateEmployeeRequestDto
{
    /// <summary>
    /// Mã nhân viên (cho phép cập nhật nếu cần chuẩn hóa, không được trùng với nhân viên khác)
    /// </summary>
    /// <example>NV00105</example>
    [Required(ErrorMessage = "Mã nhân viên không được để trống")]
    [MaxLength(15, ErrorMessage = "Mã nhân viên tối đa 15 ký tự")]
    public string Code { get; set; } = null!;

    /// <summary>
    /// Họ và tên đầy đủ
    /// </summary>
    /// <example>Nguyễn Văn Bình</example>
    [Required(ErrorMessage = "Họ và tên không được để trống")]
    [MaxLength(150, ErrorMessage = "Họ và tên tối đa 150 ký tự")]
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Ngày sinh
    /// </summary>
    /// <example>1998-10-20</example>
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// Giới tính (true: Nam, false: Nữ)
    /// </summary>
    /// <example>true</example>
    public bool? Sex { get; set; }

    /// <summary>
    /// Email liên lạc
    /// </summary>
    /// <example>binh.nguyen@company.com</example>
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [MaxLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
    public string? Email { get; set; }

    /// <summary>
    /// Ghi chú hồ sơ
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Mã phòng ban (nếu muốn chuyển phòng ban cho nhân viên)
    /// </summary>
    /// <example>2</example>
    public string? DepartmentId { get; set; }

    /// <summary>
    /// Mã chức vụ (nếu muốn bổ nhiệm/thay đổi chức vụ)
    /// </summary>
    /// <example>TP</example>
    public string? PositionId { get; set; }
}

