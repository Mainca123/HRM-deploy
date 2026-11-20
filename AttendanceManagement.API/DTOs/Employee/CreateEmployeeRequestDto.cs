using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Employee;

/// <summary>
/// DTO yêu cầu thêm mới nhân viên vào hệ thống
/// </summary>
public class CreateEmployeeRequestDto
{
    /// <summary>
    /// Mã định danh nhân viên (bắt buộc, không trùng lặp)
    /// </summary>
    /// <example>NV00105</example>
    [Required(ErrorMessage = "Mã nhân viên không được để trống")]
    [MaxLength(15, ErrorMessage = "Mã nhân viên tối đa 15 ký tự")]
    public string Code { get; set; } = null!;

    /// <summary>
    /// Họ và tên đầy đủ của nhân viên
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
    /// Địa chỉ email liên lạc
    /// </summary>
    /// <example>binh.nguyen@company.com</example>
    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [MaxLength(100, ErrorMessage = "Email tối đa 100 ký tự")]
    public string? Email { get; set; }

    /// <summary>
    /// Mã phòng ban ban đầu gán cho nhân viên
    /// </summary>
    /// <example>1</example>
    [Required(ErrorMessage = "Vui lòng chọn phòng ban khởi đầu")]
    public string DepartmentId { get; set; } = null!;

    /// <summary>
    /// Mã chức vụ ban đầu gán cho nhân viên
    /// </summary>
    /// <example>NV</example>
    [Required(ErrorMessage = "Vui lòng chọn chức vụ khởi đầu")]
    public string PositionId { get; set; } = null!;

    /// <summary>
    /// Ngày chính thức nhận việc / vào phòng ban (mặc định lấy ngày hiện tại nếu null)
    /// </summary>
    /// <example>2026-11-01</example>
    public DateTime? JoinDate { get; set; }

    /// <summary>
    /// Ghi chú thêm
    /// </summary>
    public string? Notes { get; set; }
}

