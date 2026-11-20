namespace AttendanceManagement.API.DTOs.Leave;

/// <summary>
/// DTO thông tin loại nghỉ phép (bảng Absence)
/// </summary>
public class AbsenceTypeDto
{
    /// <summary>
    /// Mã loại nghỉ phép (VD: P, O, TS, KL)
    /// </summary>
    /// <example>P</example>
    public string Id { get; set; } = null!;

    /// <summary>
    /// Tên loại nghỉ phép
    /// </summary>
    /// <example>Nghỉ phép năm</example>
    public string? Name { get; set; }

    /// <summary>
    /// Công ty chi trả lương hay không
    /// </summary>
    /// <example>true</example>
    public bool? Company { get; set; }

    /// <summary>
    /// Bảo hiểm xã hội chi trả hay không
    /// </summary>
    /// <example>false</example>
    public bool? Si { get; set; }
}

