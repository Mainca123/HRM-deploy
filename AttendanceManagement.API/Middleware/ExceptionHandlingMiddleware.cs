using System.Net;
using System.Text.Json;
using AttendanceManagement.API.DTOs.Common;

namespace AttendanceManagement.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Cho phép request tiếp tục đi qua các middleware tiếp theo
            await _next(context);
        }
        catch (Exception ex)
        {
            // Ghi log ngoại lệ ra hệ thống
            _logger.LogError(ex, "Đã xảy ra ngoại lệ chưa được xử lý: {Message}", ex.Message);

            // Bắt và xử lý ngoại lệ tập trung, trả về định dạng chuẩn ApiResponse
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Đã xảy ra lỗi trên hệ thống máy chủ. Vui lòng liên hệ quản trị viên.";
        var errors = new List<string>();

        // Phân loại mã lỗi HTTP dựa trên loại ngoại lệ phát sinh
        switch (exception)
        {
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = "Bạn không có quyền truy cập vào tài nguyên này.";
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = "Không tìm thấy dữ liệu yêu cầu.";
                break;

            case ArgumentException or BadHttpRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;

            default:
                // Nếu đang ở môi trường Development, hiển thị chi tiết Exception để dễ debug
                if (_env.IsDevelopment())
                {
                    errors.Add(exception.Message);
                    if (!string.IsNullOrEmpty(exception.StackTrace))
                    {
                        errors.Add(exception.StackTrace);
                    }
                }
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        // Chuẩn hóa định dạng trả về theo ApiResponse
        var response = ApiResponse<object>.Fail(message, errors.Count > 0 ? errors : null);

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(response, jsonOptions);
        await context.Response.WriteAsync(json);
    }
}

