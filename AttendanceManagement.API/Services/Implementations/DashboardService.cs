using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IReportRepository _reportRepository;

    public DashboardService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    private static bool IsAdminOrManager(IList<string> roles) =>
        roles.Any(r => r is "1" or "2" or "4" or "6" or "8" or "ADMIN" or "HR" or "MANAGER" or "Administration");

    public async Task<ApiResponse<EmployeeDashboardDto>> GetEmployeeDashboardAsync(int? currentEmployeeId)
    {
        if (!currentEmployeeId.HasValue)
        {
            return ApiResponse<EmployeeDashboardDto>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
        }

        var data = await _reportRepository.GetEmployeeDashboardAsync(currentEmployeeId.Value);
        return ApiResponse<EmployeeDashboardDto>.Ok(data, "Lấy thông tin Dashboard cá nhân thành công.");
    }

    public async Task<ApiResponse<ManagerDashboardDto>> GetManagerDashboardAsync(
        string? departmentId,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdminOrManager(roles))
        {
            return ApiResponse<ManagerDashboardDto>.Fail("Bạn không có quyền truy cập Dashboard Quản lý.");
        }

        var data = await _reportRepository.GetManagerDashboardAsync(departmentId);
        return ApiResponse<ManagerDashboardDto>.Ok(data, "Lấy thông tin Dashboard Quản lý thành công.");
    }

    public async Task<ApiResponse<HrDashboardDto>> GetHrDashboardAsync(IList<string> roles)
    {
        if (!roles.Any(r => r is "1" or "4" or "ADMIN" or "HR" or "Administration"))
        {
            return ApiResponse<HrDashboardDto>.Fail("Chỉ Ban Giám đốc và Phòng Nhân sự mới có quyền xem Dashboard Tổng quan công ty.");
        }

        var data = await _reportRepository.GetHrDashboardAsync();
        return ApiResponse<HrDashboardDto>.Ok(data, "Lấy thông tin Dashboard Nhân sự toàn công ty thành công.");
    }
}

