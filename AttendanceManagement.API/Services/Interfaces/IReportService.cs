using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ Báo cáo thống kê
/// </summary>
public interface IReportService
{
    Task<ApiResponse<List<LeaveSummaryReportDto>>> GetLeaveSummaryReportAsync(
        DateTime? fromDate,
        DateTime? toDate,
        string? departmentId,
        IList<string> roles);

    Task<ApiResponse<List<OvertimeSummaryReportDto>>> GetOvertimeSummaryReportAsync(
        int? month,
        int? year,
        string? departmentId,
        IList<string> roles);
}

