using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class PayrollMonthWorking
{
    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public decimal? Value { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
