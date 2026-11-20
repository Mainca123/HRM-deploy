using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO định danh khóa ghép của đơn nghỉ phép (dùng cho Phê duyệt hoặc Hủy đơn)
/// </summary>
public class LeaveRequestKeyDto
{
    /// <summary>
    /// Mã ID nhân viên
    /// </summary>
    [Required(ErrorMessage = "Mã nhân viên không được để trống")]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Ngày bắt đầu nghỉ
    /// </summary>
    [Required(ErrorMessage = "Ngày bắt đầu nghỉ không được để trống")]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Mã loại nghỉ phép
    /// </summary>
    [Required(ErrorMessage = "Mã loại nghỉ phép không được để trống")]
    public string AbsenceId { get; set; } = null!;
}

/// <summary>
/// DTO yêu cầu từ chối đơn nghỉ phép (bắt buộc nhập lý do)
/// </summary>
public class RejectLeaveRequestDto : LeaveRequestKeyDto
{
    /// <summary>
    /// Lý do từ chối đơn
    /// </summary>
    [Required(ErrorMessage = "Vui lòng nhập lý do từ chối đơn")]
    [MaxLength(300, ErrorMessage = "Lý do từ chối tối đa 300 ký tự")]
    public string RejectReason { get; set; } = null!;
}

