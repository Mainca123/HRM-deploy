using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Leave;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ Quản lý Nghỉ phép
/// </summary>
public interface ILeaveService
{
    Task<ApiResponse<List<AbsenceTypeDto>>> GetAbsenceTypesAsync();

    Task<ApiResponse<PagedResultDto<LeaveRequestDto>>> GetLeaveRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? status,
        DateTime? fromDate,
        DateTime? toDate,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> GetLeaveRequestDetailAsync(
        int employeeId,
        DateTime fromDate,
        string absenceId,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> CreateLeaveRequestAsync(
        CreateLeaveRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> UpdateLeaveRequestAsync(
        UpdateLeaveRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> ApproveLeaveRequestAsync(
        LeaveRequestKeyDto request,
        string currentUsername,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> RejectLeaveRequestAsync(
        RejectLeaveRequestDto request,
        string currentUsername,
        IList<string> roles);

    Task<ApiResponse<LeaveRequestDto>> CancelLeaveRequestAsync(
        LeaveRequestKeyDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<bool>> DeleteLeaveRequestAsync(
        int employeeId,
        DateTime fromDate,
        string absenceId,
        int? currentEmployeeId,
        IList<string> roles);

    Task<ApiResponse<LeaveBalanceDto>> GetAnnualLeaveBalanceAsync(
        int employeeId,
        short? year,
        int? currentEmployeeId,
        IList<string> roles);
}

