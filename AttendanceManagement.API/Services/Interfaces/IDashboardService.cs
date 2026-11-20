using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ Dashboard
/// </summary>
public interface IDashboardService
{
    Task<ApiResponse<EmployeeDashboardDto>> GetEmployeeDashboardAsync(int? currentEmployeeId);
    Task<ApiResponse<ManagerDashboardDto>> GetManagerDashboardAsync(string? departmentId, int? currentEmployeeId, IList<string> roles);
    Task<ApiResponse<HrDashboardDto>> GetHrDashboardAsync(IList<string> roles);
}

