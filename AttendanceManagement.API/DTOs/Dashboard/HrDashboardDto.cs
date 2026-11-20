namespace AttendanceManagement.API.DTOs.Dashboard;

/// <summary>
/// DTO Dashboard toàn công ty dành cho Giám đốc / HR
/// </summary>
public class HrDashboardDto
{
    public DateTime ReportDate { get; set; }

    // Quân số toàn công ty
    public int TotalActiveEmployees { get; set; }
    public int TotalDepartments { get; set; }
    public int PresentToday { get; set; }
    public int AbsentToday { get; set; }
    public double AttendanceRate => TotalActiveEmployees > 0
        ? Math.Round((double)PresentToday / TotalActiveEmployees * 100, 1)
        : 0;

    // Tổng hợp tháng hiện tại
    public int Month { get; set; }
    public int Year { get; set; }
    public int TotalNewEmployeesThisMonth { get; set; }
    public decimal TotalOvertimeHoursThisMonth { get; set; }
    public decimal TotalLeaveDaysThisMonth { get; set; }

    // Thống kê theo phòng ban
    public List<DepartmentAttendanceStatDto> DepartmentStats { get; set; } = new();
}

public class DepartmentAttendanceStatDto
{
    public string DepartmentId { get; set; } = null!;
    public string DepartmentName { get; set; } = null!;
    public int TotalEmployees { get; set; }
    public int PresentToday { get; set; }
    public double AttendanceRate => TotalEmployees > 0
        ? Math.Round((double)PresentToday / TotalEmployees * 100, 1)
        : 0;
}

