namespace AttendanceManagement.API.DTOs.Overtime;

/// <summary>
/// DTO thông tin loại đăng ký tăng ca và cấu hình giới hạn tăng ca từ bảng OvertimeLimit
/// </summary>
public class OvertimeTypeDto
{
    /// <summary>
    /// Mã loại tăng ca (Type trong bảng RegisterOvertime)
    /// </summary>
    /// <example>3</example>
    public byte Type { get; set; }

    /// <summary>
    /// Tên loại tăng ca
    /// </summary>
    /// <example>Đăng ký tăng ca theo ca / ngày</example>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả ý nghĩa giá trị Value
    /// </summary>
    public string Description { get; set; } = null!;
}

/// <summary>
/// DTO cấu hình giới hạn giờ tăng ca (bảng OvertimeLimit)
/// </summary>
public class OvertimeLimitDto
{
    public string ShiftId { get; set; } = null!;
    public string ReplacementShiftId { get; set; } = null!;
    public DateOnly FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public DateTime? FromTime { get; set; }
    public DateTime? ToTime { get; set; }
    public decimal? Value { get; set; }
    public string? EmployeeIds { get; set; }
}

/// <summary>
/// DTO tổng hợp danh mục loại tăng ca và danh sách giới hạn cấu hình
/// </summary>
public class OvertimeTypesResponseDto
{
    public List<OvertimeTypeDto> Types { get; set; } = new();
    public List<OvertimeLimitDto> Limits { get; set; } = new();
}

