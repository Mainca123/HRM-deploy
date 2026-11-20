using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Common;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ Chấm công
/// </summary>
public interface IAttendanceService
{
    // 1. Ca làm việc
    Task<ApiResponse<List<ShiftDto>>> GetShiftsAsync();

    // 2. Vào / ra hàng ngày
    Task<ApiResponse<PagedResultDto<TimeInTimeOutDto>>> GetDailyAttendancePagedAsync(
        int pageIndex,
        int pageSize,
        DateTime? date,
        string? departmentId,
        string? search,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<List<TimeInTimeOutDto>>> GetMyAttendanceHistoryAsync(
        int? currentEmployeeId,
        int? month,
        int? year);

    // 3. Check-in / Check-out
    Task<ApiResponse<TimeInTimeOutDto>> CheckInAsync(
        CheckInRequestDto request,
        int? currentEmployeeId,
        string currentUsername);

    Task<ApiResponse<TimeInTimeOutDto>> CheckOutAsync(
        CheckOutRequestDto request,
        int? currentEmployeeId,
        string currentUsername);

    // 4. Giải trình đi muộn / về sớm
    Task<ApiResponse<PagedResultDto<RegisterLateEarlyDto>>> GetLateEarlyRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        DateTime? fromDate,
        DateTime? toDate,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<RegisterLateEarlyDto>> CreateLateEarlyRequestAsync(
        CreateLateEarlyRequestDto request,
        int? currentEmployeeId,
        string currentUsername);

    Task<ApiResponse<RegisterLateEarlyDto>> ApproveLateEarlyRequestAsync(
        ApproveLateEarlyRequestDto request,
        string currentUsername,
        IList<string> roles);

    Task<ApiResponse<bool>> DeleteLateEarlyRequestAsync(
        int employeeId,
        DateTime fromDate,
        bool type,
        int? currentEmployeeId,
        IList<string> roles);

    // 5. Bảng tổng hợp công tháng
    Task<ApiResponse<PagedResultDto<MonthlyAttendanceSummaryDto>>> GetMonthlyAttendanceSummaryPagedAsync(
        int pageIndex,
        int pageSize,
        int? month,
        int? year,
        string? departmentId,
        string? search,
        IList<string> roles);
}

