using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO yêu cầu cập nhật đơn xin nghỉ phép (chỉ cho phép sửa khi đơn ở trạng thái Chờ duyệt)
/// </summary>
public class UpdateLeaveRequestDto
{
    /// <summary>
    /// Mã ID nhân viên của đơn (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Mã nhân viên không được để trống")]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Ngày bắt đầu nghỉ của đơn cần sửa (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Ngày bắt đầu nghỉ không được để trống")]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Mã loại nghỉ phép của đơn cần sửa (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Mã loại nghỉ phép không được để trống")]
    public string AbsenceId { get; set; } = null!;

    /// <summary>
    /// Ngày kết thúc nghỉ mới
    /// </summary>
    [Required(ErrorMessage = "Ngày kết thúc nghỉ không được để trống")]
    public DateTime ToDate { get; set; }

    /// <summary>
    /// Số ngày nghỉ mới
    /// </summary>
    [Required(ErrorMessage = "Số ngày nghỉ không được để trống")]
    [Range(0.1, 365, ErrorMessage = "Số ngày nghỉ phải lớn hơn 0 và không vượt quá 365")]
    public decimal Unit { get; set; }

    /// <summary>
    /// Lý do / Ghi chú mới
    /// </summary>
    [MaxLength(500, ErrorMessage = "Ghi chú tối đa 500 ký tự")]
    public string? Notes { get; set; }
}

