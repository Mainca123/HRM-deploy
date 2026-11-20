using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class MonthAttendance
{
    public int EmployeeId { get; set; }

    public DateTime WorkingDay { get; set; }

    public string DepartmentId { get; set; } = null!;

    public decimal? Value { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public string? Text { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
