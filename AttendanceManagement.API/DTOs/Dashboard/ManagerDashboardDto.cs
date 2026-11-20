using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Leave;

namespace AttendanceManagement.API.DTOs.Dashboard;

/// <summary>
/// DTO Dashboard Quản lý phòng ban / phân xưởng
/// </summary>
public class ManagerDashboardDto
{
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public DateTime ReportDate { get; set; }

    // Chỉ số hôm nay
    public int TotalDepartmentEmployees { get; set; }
    public int PresentToday { get; set; }
    public int AbsentToday { get; set; }
    public double AttendanceRate => TotalDepartmentEmployees > 0
        ? Math.Round((double)PresentToday / TotalDepartmentEmployees * 100, 1)
        : 0;

    // Đơn cần duyệt
    public int PendingLeaveRequestsCount { get; set; }
    public int PendingLateEarlyRequestsCount { get; set; }

    // Danh sách chờ xử lý
    public List<LeaveRequestDto> PendingLeaves { get; set; } = new();
    public List<RegisterLateEarlyDto> PendingLateEarlies { get; set; } = new();
}

