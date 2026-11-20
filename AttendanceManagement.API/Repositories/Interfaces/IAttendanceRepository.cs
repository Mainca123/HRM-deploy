using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu Chấm công (Shift, TimeInTimeOut, RegisterLateEarly, MonthAttendance)
/// </summary>
public interface IAttendanceRepository
{
    // 1. Ca làm việc (Shift)
    Task<List<ShiftDto>> GetShiftsAsync();
    Task<Shift?> GetShiftByIdAsync(string shiftId);
    Task<Shift?> GetDefaultShiftAsync();

    // 2. Dữ liệu quẹt thẻ vào/ra (TimeInTimeOut)
    Task<PagedResultDto<TimeInTimeOutDto>> GetDailyAttendancePagedAsync(
        int pageIndex,
        int pageSize,
        DateTime date,
        string? departmentId,
        string? search);

    Task<List<TimeInTimeOutDto>> GetMyAttendanceHistoryAsync(int employeeId, int month, int year);
    Task<TimeInTimeOut?> GetTodayTimeRecordAsync(int employeeId, DateTime workingDay, string? shiftId = null);
    Task CreateTimeInTimeOutAsync(TimeInTimeOut record);
    Task UpdateTimeInTimeOutAsync(TimeInTimeOut record);

    // 3. Giải trình đi muộn / về sớm (RegisterLateEarly)
    Task<PagedResultDto<RegisterLateEarlyDto>> GetLateEarlyRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        DateTime? fromDate,
        DateTime? toDate);

    Task<RegisterLateEarly?> GetLateEarlyEntityAsync(int employeeId, DateTime fromDate, bool type);
    Task CreateLateEarlyAsync(RegisterLateEarly entity);
    Task UpdateLateEarlyAsync(RegisterLateEarly entity);
    Task DeleteLateEarlyAsync(RegisterLateEarly entity);

    // 4. Bảng tổng hợp công tháng (MonthAttendance)
    Task<PagedResultDto<MonthlyAttendanceSummaryDto>> GetMonthlyAttendanceSummaryPagedAsync(
        int pageIndex,
        int pageSize,
        int month,
        int year,
        string? departmentId,
        string? search);
}

