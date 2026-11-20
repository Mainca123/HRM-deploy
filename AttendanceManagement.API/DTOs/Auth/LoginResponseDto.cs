namespace AttendanceManagement.API.DTOs.Auth;

/// <summary>
/// Dữ liệu phản hồi sau khi đăng nhập thành công
/// </summary>
public class LoginResponseDto
{
    /// <summary>
    /// Chuỗi mã hóa JWT Access Token dùng để xác thực trong các yêu cầu API tiếp theo
    /// </summary>
    /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Loại mã token xác thực (mặc định là Bearer)
    /// </summary>
    /// <example>Bearer</example>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>
    /// Thời điểm hết hạn của Token theo giờ UTC
    /// </summary>
    /// <example>2026-11-14T19:00:00Z</example>
    public DateTime ExpiresAt { get; set; }

    /// <summary>
    /// Thông tin hồ sơ của tài khoản và nhân viên đang đăng nhập
    /// </summary>
    public UserProfileDto User { get; set; } = null!;
}

/// <summary>
/// Thông tin hồ sơ tóm tắt của tài khoản và nhân sự liên kết
/// </summary>
public class UserProfileDto
{
    /// <summary>
    /// Tên đăng nhập tài khoản
    /// </summary>
    /// <example>admin</example>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Mã ID nhân viên trong bảng Employee (nếu tài khoản đã được liên kết)
    /// </summary>
    /// <example>101</example>
    public int? EmployeeId { get; set; }

    /// <summary>
    /// Mã định danh nhân viên quản lý nội bộ (nếu có)
    /// </summary>
    /// <example>NV00101</example>
    public string? EmployeeCode { get; set; }

    /// <summary>
    /// Họ và tên đầy đủ của nhân viên
    /// </summary>
    /// <example>Nguyễn Văn Quản Trị</example>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Vai trò / Nhóm quyền hạn của tài khoản (ADMIN, HR, MANAGER, EMPLOYEE)
    /// </summary>
    /// <example>ADMIN</example>
    public string? Role { get; set; }
}
