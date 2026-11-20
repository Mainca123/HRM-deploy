using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SalaryTable
{
    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string SalaryItemId { get; set; } = null!;

    public string? Value { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
