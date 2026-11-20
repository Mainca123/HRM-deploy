using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Overtime;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ Quản lý Làm thêm giờ (Overtime)
/// </summary>
public interface IOvertimeService
{
    Task<ApiResponse<OvertimeTypesResponseDto>> GetOvertimeTypesAsync();

    Task<ApiResponse<PagedResultDto<OvertimeRequestDto>>> GetOvertimeRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? type,
        DateOnly? fromDate,
        DateOnly? toDate,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<OvertimeRequestDto>> GetOvertimeRequestDetailAsync(
        int employeeId,
        DateOnly fromDate,
        byte type,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<OvertimeRequestDto>> CreateOvertimeRequestAsync(
        CreateOvertimeRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<OvertimeRequestDto>> UpdateOvertimeRequestAsync(
        UpdateOvertimeRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<bool>> DeleteOvertimeRequestAsync(
        int employeeId,
        DateOnly fromDate,
        byte type,
        int? currentEmployeeId,
        IList<string> roles);
}

