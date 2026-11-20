namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO thông tin đơn xin nghỉ phép (dùng cho cả danh sách và chi tiết)
/// </summary>
public class LeaveRequestDto
{
    /// <summary>
    /// Mã ID nhân viên
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
    /// Ngày bắt đầu nghỉ (Khóa chính ghép)
    /// </summary>
    public DateTime FromDate { get; set; }

    /// <summary>
    /// Ngày kết thúc nghỉ
    /// </summary>
    public DateTime? ToDate { get; set; }

    /// <summary>
    /// Số ngày/giờ nghỉ (VD: 1.0, 0.5)
    /// </summary>
    public decimal? Unit { get; set; }

    /// <summary>
    /// Mã loại nghỉ phép (Khóa chính ghép)
    /// </summary>
    public string AbsenceId { get; set; } = null!;

    /// <summary>
    /// Tên loại nghỉ phép
    /// </summary>
    public string? AbsenceName { get; set; }

    /// <summary>
    /// Lý do / Ghi chú đơn nghỉ
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Trạng thái đơn: 0 (Chờ duyệt), 1 (Đã duyệt), 2 (Từ chối), 3 (Đã hủy)
    /// </summary>
    public byte? Status { get; set; }

    /// <summary>
    /// Tên hiển thị của trạng thái đơn
    /// </summary>
    public string StatusName => Status switch
    {
        0 => "Chờ duyệt",
        1 => "Đã duyệt",
        2 => "Từ chối",
        3 => "Đã hủy",
        _ => "Không xác định"
    };

    /// <summary>
    /// Người tạo đơn
    /// </summary>
    public string? CreatedUserName { get; set; }

    /// <summary>
    /// Thời điểm tạo đơn
    /// </summary>
    public DateTime? CreatedDate { get; set; }

    /// <summary>
    /// Người chỉnh sửa gần nhất
    /// </summary>
    public string? ModifiedUserName { get; set; }

    /// <summary>
    /// Thời điểm chỉnh sửa gần nhất
    /// </summary>
    public DateTime? ModifiedDate { get; set; }

    /// <summary>
    /// Quản trị viên đã duyệt hoặc từ chối đơn
    /// </summary>
    public string? ApprovedUserName { get; set; }

    /// <summary>
    /// Thời điểm duyệt hoặc từ chối đơn
    /// </summary>
    public DateTime? ApprovedDate { get; set; }
}

