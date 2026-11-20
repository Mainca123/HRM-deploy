using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Overtime;
using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu Làm thêm giờ (RegisterOvertime, OvertimeLimit)
/// </summary>
public interface IOvertimeRepository
{
    /// <summary>
    /// Lấy danh sách cấu hình giới hạn tăng ca từ bảng OvertimeLimit
    /// </summary>
    Task<List<OvertimeLimit>> GetOvertimeLimitsAsync();

    /// <summary>
    /// Lấy danh sách đăng ký tăng ca có phân trang và lọc
    /// </summary>
    Task<PagedResultDto<OvertimeRequestDto>> GetOvertimeRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? type,
        DateOnly? fromDate,
        DateOnly? toDate);

    /// <summary>
    /// Lấy chi tiết 1 bản ghi tăng ca theo khóa chính ghép (EmployeeId, FromDate, Type)
    /// </summary>
    Task<OvertimeRequestDto?> GetOvertimeRequestDetailAsync(int employeeId, DateOnly fromDate, byte type);

    /// <summary>
    /// Tìm thực thể RegisterOvertime theo khóa chính ghép để sửa hoặc xóa
    /// </summary>
    Task<RegisterOvertime?> GetOvertimeRequestEntityAsync(int employeeId, DateOnly fromDate, byte type);

    /// <summary>
    /// Kiểm tra bản ghi đăng ký tăng ca có tồn tại không
    /// </summary>
    Task<bool> ExistsOvertimeRequestAsync(int employeeId, DateOnly fromDate, byte type);

    /// <summary>
    /// Tạo mới bản ghi đăng ký tăng ca
    /// </summary>
    Task CreateOvertimeRequestAsync(RegisterOvertime entity);

    /// <summary>
    /// Cập nhật / xác nhận bản ghi đăng ký tăng ca
    /// </summary>
    Task UpdateOvertimeRequestAsync(RegisterOvertime entity);

    /// <summary>
    /// Xóa bản ghi đăng ký tăng ca
    /// </summary>
    Task DeleteOvertimeRequestAsync(RegisterOvertime entity);
}

