namespace AttendanceManagement.API.DTOs.Overtime;

/// <summary>
/// DTO thông tin bản ghi đăng ký làm thêm giờ (bảng RegisterOvertime)
/// </summary>
public class OvertimeRequestDto
{
    /// <summary>
    /// Mã ID nhân viên (Khóa chính ghép)
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Mã định danh nhân viên
    /// </summary>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// Họ và tên nhân viên
    /// </summary>
    public string EmployeeFullName { get; set; } = null!;

    /// <summary>
    /// Tên phòng ban hiện tại
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// Ngày bắt đầu áp dụng (Khóa chính ghép, yyyy-MM-dd)
    /// </summary>
    public DateOnly FromDate { get; set; }

    /// <summary>
    /// Ngày kết thúc áp dụng (yyyy-MM-dd, có thể là 9999-12-31 nếu vô thời hạn)
    /// </summary>
    public DateOnly? ToDate { get; set; }

    /// <summary>
    /// Loại tăng ca (Khóa chính ghép)
    /// </summary>
    public byte Type { get; set; }

    /// <summary>
    /// Số giờ tăng ca đăng ký ( > 0: số giờ cụ thể; 0: theo giờ thực tế; -1: không tăng ca)
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Người tạo bản ghi
    /// </summary>
    public string? CreatedUserName { get; set; }

    /// <summary>
    /// Thời điểm tạo
    /// </summary>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Người cập nhật / xác nhận gần nhất
    /// </summary>
    public string? ModifiedUserName { get; set; }

    /// <summary>
    /// Thời điểm cập nhật / xác nhận gần nhất
    /// </summary>
    public DateTime? ModifiedDate { get; set; }
}

