using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SalaryProcess
{
    public int EmployeeId { get; set; }

    public decimal? BasicSalary { get; set; }

    public decimal? InsuranceSalary { get; set; }

    public string? SalaryTypeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Reason { get; set; }

    public string? DecisionId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
