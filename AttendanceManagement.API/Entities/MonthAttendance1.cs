using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class MonthAttendance1
{
    public int Spid { get; set; }

    public int EmployeeId { get; set; }

    public DateTime WorkingDay { get; set; }

    public string? DepartmentId { get; set; }

    public decimal? Value { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public string? Text { get; set; }
}
