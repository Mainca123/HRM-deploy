namespace AttendanceManagement.API.DTOs.Attendance;

/// <summary>
/// DTO dữ liệu quẹt thẻ vào/ra thực tế của nhân viên
/// </summary>
public class TimeInTimeOutDto
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentName { get; set; }
    public DateTime WorkingDay { get; set; }
    public string ShiftId { get; set; } = null!;
    public string? ShiftName { get; set; }
    public DateTime? TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    public byte? Status { get; set; }
    public string? Notes { get; set; }

    // Phân tích giờ thực tế so với giờ chuẩn của ca
    public int? LateMinutes { get; set; }
    public int? EarlyMinutes { get; set; }
    public double? WorkingHours { get; set; }
}

