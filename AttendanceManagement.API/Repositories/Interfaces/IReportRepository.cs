using AttendanceManagement.API.DTOs.Dashboard;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu Thống kê và Báo cáo tổng hợp
/// </summary>
public interface IReportRepository
{
    // Dashboard số liệu
    Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(int employeeId);
    Task<ManagerDashboardDto> GetManagerDashboardAsync(string? departmentId);
    Task<HrDashboardDto> GetHrDashboardAsync();

    // Báo cáo nghiệp vụ
    Task<List<LeaveSummaryReportDto>> GetLeaveSummaryReportAsync(DateTime fromDate, DateTime toDate, string? departmentId);
    Task<List<OvertimeSummaryReportDto>> GetOvertimeSummaryReportAsync(int month, int year, string? departmentId);
}

