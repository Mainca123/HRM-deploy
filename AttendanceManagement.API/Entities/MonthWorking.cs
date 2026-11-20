using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class MonthWorking
{
    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public decimal? Value { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
