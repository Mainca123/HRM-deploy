namespace AttendanceManagement.API.DTOs.Common;

/// <summary>
/// Cấu trúc chuẩn hóa dữ liệu trả về cho toàn bộ API trong hệ thống
/// </summary>
/// <typeparam name="T">Kiểu dữ liệu của phần dữ liệu chính (Data)</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Trạng thái thành công của yêu cầu: true nếu thành công, false nếu có lỗi
    /// </summary>
    /// <example>true</example>
    public bool Success { get; set; }

    /// <summary>
    /// Thông điệp mô tả kết quả xử lý (dành cho người dùng hoặc lập trình viên đọc)
    /// </summary>
    /// <example>Đăng nhập thành công!</example>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Dữ liệu chính trả về của API (có thể là đối tượng, danh sách hoặc null khi lỗi)
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Danh sách chi tiết các lỗi phát sinh (nếu có)
    /// </summary>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Khởi tạo nhanh một phản hồi thành công
    /// </summary>
    /// <param name="data">Dữ liệu cần trả về</param>
    /// <param name="message">Thông báo thành công</param>
    public static ApiResponse<T> Ok(T data, string message = "Thành công")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Khởi tạo nhanh một phản hồi thất bại kèm thông báo lỗi
    /// </summary>
    /// <param name="message">Thông điệp lỗi chính</param>
    /// <param name="errors">Danh sách các lỗi chi tiết (nếu có)</param>
    public static ApiResponse<T> Fail(string message, List<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }
}
