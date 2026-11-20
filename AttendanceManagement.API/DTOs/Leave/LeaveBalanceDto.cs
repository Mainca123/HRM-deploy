namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO tra cứu số dư phép năm của nhân viên
/// </summary>
public class LeaveBalanceDto
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
    /// Năm tra cứu
    /// </summary>
    public short Year { get; set; }

    /// <summary>
    /// Tổng số ngày phép được cấp trong năm (từ bảng AnnualLeave)
    /// </summary>
    public decimal TotalAllocated { get; set; }

    /// <summary>
    /// Tổng số ngày nghỉ đã được duyệt trong năm
    /// </summary>
    public decimal UsedDays { get; set; }

    /// <summary>
    /// Tổng số ngày nghỉ đang chờ duyệt
    /// </summary>
    public decimal PendingDays { get; set; }

    /// <summary>
    /// Số ngày phép còn lại (TotalAllocated - UsedDays)
    /// </summary>
    public decimal RemainingDays => TotalAllocated - UsedDays;
}

