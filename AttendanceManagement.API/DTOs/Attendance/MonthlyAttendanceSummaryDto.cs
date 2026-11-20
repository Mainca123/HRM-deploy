namespace AttendanceManagement.API.DTOs.Attendance;

/// <summary>
/// DTO dòng tổng hợp công tháng của 1 nhân viên (được xoay từ bảng MonthAttendance)
/// </summary>
public class MonthlyAttendanceSummaryDto
{
    public int EmployeeId { get; set; }
    public string? EmployeeCode { get; set; }
    public string EmployeeFullName { get; set; } = null!;
    public string? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

    /// <summary>
    /// Tổng số ngày công chuẩn làm việc trong tháng (AttendanceItemID = 'NC')
    /// </summary>
    public decimal TotalWorkDays { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca ngày thường (AttendanceItemID = 'OT1')
    /// </summary>
    public decimal TotalOvertimeNormal { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca đêm ngày thường (AttendanceItemID = 'OT2')
    /// </summary>
    public decimal TotalOvertimeNormalNight { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca ngày nghỉ cuối tuần (AttendanceItemID = 'OT3')
    /// </summary>
    public decimal TotalOvertimeWeekend { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca đêm ngày nghỉ cuối tuần (AttendanceItemID = 'OT4')
    /// </summary>
    public decimal TotalOvertimeWeekendNight { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca ngày lễ/tết (AttendanceItemID = 'OT5')
    /// </summary>
    public decimal TotalOvertimeHoliday { get; set; }

    /// <summary>
    /// Tổng số giờ tăng ca đêm ngày lễ/tết (AttendanceItemID = 'OT6')
    /// </summary>
    public decimal TotalOvertimeHolidayNight { get; set; }

    /// <summary>
    /// Tổng số phút đi muộn trong tháng (AttendanceItemID = 'DM')
    /// </summary>
    public decimal TotalLateMinutes { get; set; }

    /// <summary>
    /// Tổng số phút về sớm trong tháng (AttendanceItemID = 'VS')
    /// </summary>
    public decimal TotalEarlyMinutes { get; set; }

    /// <summary>
    /// Tổng tiền ăn ca trong tháng (AttendanceItemID = 'TA')
    /// </summary>
    public decimal TotalMealAllowance { get; set; }
}

