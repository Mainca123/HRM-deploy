using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO yêu cầu tạo mới đơn xin nghỉ phép
/// </summary>
public class CreateLeaveRequestDto
{
    /// <summary>
    /// Mã ID nhân viên (Chỉ Admin có quyền truyền ID nhân viên khác để tạo hộ; các tài khoản thường hệ thống tự lấy theo Token)
    /// </summary>
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Ngày bắt đầu nghỉ (Khóa chính ghép)
    /// </summary>
    [Required(ErrorMessage = "Ngày bắt đầu nghỉ không được để trống")]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Ngày kết thúc nghỉ
    /// </summary>
    [Required(ErrorMessage = "Ngày kết thúc nghỉ không được để trống")]
    public DateTime ToDate { get; set; }

    /// <summary>
    /// Số ngày nghỉ (VD: 1.0, 0.5, 2.0)
    /// </summary>
    [Required(ErrorMessage = "Số ngày nghỉ không được để trống")]
    [Range(0.1, 365, ErrorMessage = "Số ngày nghỉ phải lớn hơn 0 và không vượt quá 365")]
    public decimal Unit { get; set; }

    /// <summary>
    /// Mã loại nghỉ phép (FK tới bảng Absence)
    /// </summary>
    [Required(ErrorMessage = "Vui lòng chọn loại nghỉ phép")]
    [MaxLength(15, ErrorMessage = "Mã loại nghỉ phép tối đa 15 ký tự")]
    public string AbsenceId { get; set; } = null!;

    /// <summary>
    /// Lý do xin nghỉ
    /// </summary>
    [MaxLength(500, ErrorMessage = "Lý do nghỉ tối đa 500 ký tự")]
    public string? Notes { get; set; }
}

