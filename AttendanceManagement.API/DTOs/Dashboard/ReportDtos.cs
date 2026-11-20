namespace AttendanceManagement.API.DTOs.Dashboard;

/// <summary>
/// DTO Báo cáo tổng hợp số ngày nghỉ phép theo loại và phòng ban
/// </summary>
public class LeaveSummaryReportDto
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public decimal AnnualLeaveDays { get; set; }       // P - Nghỉ phép năm
    public decimal SickLeaveDays { get; set; }         // O - Nghỉ ốm
    public decimal UnpaidLeaveDays { get; set; }       // KL - Nghỉ không lương
    public decimal MaternityLeaveDays { get; set; }    // NS / TS - Thai sản
    public decimal OtherLeaveDays { get; set; }        // Các loại khác
    public decimal TotalLeaveDays => AnnualLeaveDays + SickLeaveDays + UnpaidLeaveDays + MaternityLeaveDays + OtherLeaveDays;
}

/// <summary>
/// DTO Báo cáo tổng hợp giờ làm thêm giờ theo phòng ban
/// </summary>
public class OvertimeSummaryReportDto
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }

    public decimal NormalOvertimeHours { get; set; }   // OT1
    public decimal NightOvertimeHours { get; set; }    // OT2
    public decimal WeekendOvertimeHours { get; set; }  // OT3 / OT4
    public decimal HolidayOvertimeHours { get; set; }  // OT5 / OT6
    public decimal TotalOvertimeHours => NormalOvertimeHours + NightOvertimeHours + WeekendOvertimeHours + HolidayOvertimeHours;
}

