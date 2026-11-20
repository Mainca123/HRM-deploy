namespace AttendanceManagement.API.DTOs.Attendance;

/// <summary>
/// DTO yêu cầu Check-in trực tuyến
/// </summary>
public class CheckInRequestDto
{
    /// <summary>
    /// Mã ca làm việc (tùy chọn; nếu để trống hệ thống tự lấy ca mặc định IsDefault = true)
    /// </summary>
    public string? ShiftId { get; set; }

    /// <summary>
    /// Ghi chú khi Check-in (ví dụ vị trí hoặc lý do)
    /// </summary>
    public string? Notes { get; set; }
}

/// <summary>
/// DTO yêu cầu Check-out trực tuyến
/// </summary>
public class CheckOutRequestDto
{
    /// <summary>
    /// Ghi chú khi Check-out
    /// </summary>
    public string? Notes { get; set; }
}

