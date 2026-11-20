using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AuditMonthSalary
{
    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string SalaryItemId { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
