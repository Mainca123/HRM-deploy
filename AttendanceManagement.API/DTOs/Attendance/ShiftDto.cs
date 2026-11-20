namespace AttendanceManagement.API.DTOs.Attendance;

/// <summary>
/// DTO thông tin ca làm việc kèm khung giờ chi tiết
/// </summary>
public class ShiftDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool? IsDefault { get; set; }
    public bool? IsNightShift { get; set; }

    // Khung giờ chi tiết từ ShiftDetail (nếu có)
    public DateTime? NormalTimeIn { get; set; }
    public DateTime? NormalTimeOut { get; set; }
    public DateTime? NormalBreakOut { get; set; }
    public DateTime? NormalBreakIn { get; set; }
    public int? NormalTotalMinutes { get; set; }
    public int? LateComingMinutes { get; set; }
    public int? EarlyReturningMinutes { get; set; }
}

