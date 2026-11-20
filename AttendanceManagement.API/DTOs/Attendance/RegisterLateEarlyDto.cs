using System.ComponentModel.DataAnnotations;

namespace AttendanceManagement.API.DTOs.Attendance;

/// <summary>
/// DTO thông tin đơn giải trình đi muộn / về sớm
/// </summary>
public class RegisterLateEarlyDto
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentName { get; set; }
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public bool Type { get; set; }
    public string TypeName => Type ? "Về sớm" : "Đi muộn";
    public int? Value { get; set; } // Số phút
    public string? Reason { get; set; }
    public string? CreatedUserName { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedUserName { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsApproved => !string.IsNullOrEmpty(ModifiedUserName);
}

/// <summary>
/// DTO gửi đơn giải trình đi muộn / về sớm
/// </summary>
public class CreateLateEarlyRequestDto
{
    [Required(ErrorMessage = "Ngày phát sinh không được để trống")]
    public DateTime FromDate { get; set; }

    /// <summary>
    /// false: Đi muộn (0), true: Về sớm (1)
    /// </summary>
    [Required]
    public bool Type { get; set; }

    /// <summary>
    /// Số phút đi muộn hoặc về sớm cần giải trình
    /// </summary>
    [Range(1, 480, ErrorMessage = "Số phút phải từ 1 đến 480 phút")]
    public int Value { get; set; }

    [Required(ErrorMessage = "Lý do giải trình không được để trống")]
    [MaxLength(250, ErrorMessage = "Lý do tối đa 250 ký tự")]
    public string Reason { get; set; } = null!;
}

/// <summary>
/// DTO phê duyệt giải trình đi muộn / về sớm
/// </summary>
public class ApproveLateEarlyRequestDto
{
    [Required]
    public int EmployeeId { get; set; }

    [Required]
    public DateTime FromDate { get; set; }

    [Required]
    public bool Type { get; set; }

    [MaxLength(200)]
    public string? AdminNote { get; set; }
}

