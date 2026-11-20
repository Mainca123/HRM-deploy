using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class PayrollMonthAttendance
{
    public int EmployeeId { get; set; }

    public DateOnly WorkingDay { get; set; }

    public string? DepartmentId { get; set; }

    public decimal? Value { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public string? Text { get; set; }
}
