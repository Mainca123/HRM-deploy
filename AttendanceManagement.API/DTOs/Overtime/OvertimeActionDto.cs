using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Overtime;

/// <summary>
/// DTO yêu cầu đăng ký làm thêm giờ (bảng RegisterOvertime)
/// </summary>
public class CreateOvertimeRequestDto
{
    /// <summary>
    /// Mã ID nhân viên (Admin có thể truyền ID nhân viên khác; các nhóm khác hệ thống tự lấy theo Token)
    /// </summary>
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Ngày bắt đầu tăng ca (yyyy-MM-dd)
    /// </summary>
    [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
    public DateOnly FromDate { get; set; }

    /// <summary>
    /// Ngày kết thúc tăng ca (yyyy-MM-dd)
    /// </summary>
    public DateOnly? ToDate { get; set; }

    /// <summary>
    /// Loại tăng ca (trong DB mặc định dùng Type = 3, hoặc 1..4)
    /// </summary>
    [Required(ErrorMessage = "Loại tăng ca không được để trống")]
    [Range(1, 10, ErrorMessage = "Loại tăng ca phải nằm trong khoảng 1 đến 10")]
    public byte Type { get; set; } = 3;

    /// <summary>
    /// Số giờ tăng ca (> 0: số giờ tối đa; 0: theo thực tế máy chấm công; -1: không tăng ca)
    /// </summary>
    [Required(ErrorMessage = "Giá trị giờ tăng ca không được để trống")]
    [Range(-1, 24, ErrorMessage = "Giá trị giờ tăng ca phải từ -1 đến 24")]
    public decimal Value { get; set; }
}

/// <summary>
/// DTO yêu cầu cập nhật / xác nhận đăng ký làm thêm giờ
/// </summary>
public class UpdateOvertimeRequestDto
{
    /// <summary>
    /// Mã ID nhân viên (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Mã nhân viên không được để trống")]
    public int EmployeeId { get; set; }

    /// <summary>
    /// Ngày bắt đầu tăng ca của bản ghi cần sửa (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
    public DateOnly FromDate { get; set; }

    /// <summary>
    /// Loại tăng ca của bản ghi cần sửa (Khóa ghép)
    /// </summary>
    [Required(ErrorMessage = "Loại tăng ca không được để trống")]
    public byte Type { get; set; }

    /// <summary>
    /// Ngày kết thúc mới
    /// </summary>
    public DateOnly? ToDate { get; set; }

    /// <summary>
    /// Số giờ tăng ca mới (-1 đến 24)
    /// </summary>
    [Required(ErrorMessage = "Giá trị giờ tăng ca không được để trống")]
    [Range(-1, 24, ErrorMessage = "Giá trị giờ tăng ca phải từ -1 đến 24")]
    public decimal Value { get; set; }
}

