using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    private static bool IsAdminOrManager(IList<string> roles) =>
        roles.Any(r => r is "1" or "2" or "4" or "6" or "8" or "ADMIN" or "HR" or "MANAGER" or "Administration");

    public async Task<ApiResponse<List<LeaveSummaryReportDto>>> GetLeaveSummaryReportAsync(
        DateTime? fromDate,
        DateTime? toDate,
        string? departmentId,
        IList<string> roles)
    {
        if (!IsAdminOrManager(roles))
        {
            return ApiResponse<List<LeaveSummaryReportDto>>.Fail("Bạn không có quyền truy cập Báo cáo nghỉ phép.");
        }

        var now = DateTime.Now;
        var start = fromDate ?? new DateTime(now.Year, now.Month, 1);
        var end = toDate ?? start.AddMonths(1).AddTicks(-1);

        if (end < start)
        {
            return ApiResponse<List<LeaveSummaryReportDto>>.Fail("Ngày kết thúc (toDate) phải lớn hơn hoặc bằng ngày bắt đầu (fromDate).");
        }

        var report = await _reportRepository.GetLeaveSummaryReportAsync(start, end, departmentId);
        return ApiResponse<List<LeaveSummaryReportDto>>.Ok(report, $"Xuất báo cáo tổng hợp nghỉ phép từ {start:yyyy-MM-dd} đến {end:yyyy-MM-dd} thành công.");
    }

    public async Task<ApiResponse<List<OvertimeSummaryReportDto>>> GetOvertimeSummaryReportAsync(
        int? month,
        int? year,
        string? departmentId,
        IList<string> roles)
    {
        if (!IsAdminOrManager(roles))
        {
            return ApiResponse<List<OvertimeSummaryReportDto>>.Fail("Bạn không có quyền truy cập Báo cáo làm thêm giờ.");
        }

        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        var report = await _reportRepository.GetOvertimeSummaryReportAsync(targetMonth, targetYear, departmentId);
        return ApiResponse<List<OvertimeSummaryReportDto>>.Ok(report, $"Xuất báo cáo tổng hợp làm thêm giờ tháng {targetMonth}/{targetYear} thành công.");
    }
}

