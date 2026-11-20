using AttendanceManagement.API.DTOs.Leave;

namespace AttendanceManagement.API.DTOs.Dashboard;

/// <summary>
/// DTO Dashboard cá nhân dành cho Nhân viên
/// </summary>
public class EmployeeDashboardDto
{
    public int EmployeeId { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentName { get; set; }
    public string? PositionName { get; set; }
    public int CurrentMonth { get; set; }
    public int CurrentYear { get; set; }

    // Thống kê tháng này
    public decimal WorkDaysThisMonth { get; set; }
    public decimal OvertimeHoursThisMonth { get; set; }
    public decimal AnnualLeaveBalance { get; set; }
    public int LateMinutesThisMonth { get; set; }

    // Trạng thái quẹt thẻ hôm nay
    public DateTime? TodayTimeIn { get; set; }
    public DateTime? TodayTimeOut { get; set; }
    public string? TodayShiftName { get; set; }

    // 5 đơn nghỉ phép gần nhất
    public List<LeaveRequestDto> RecentLeaveRequests { get; set; } = new();
}

