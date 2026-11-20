using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Resignation
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? Decision { get; set; }

    public string? ResignationReasonId { get; set; }

    public decimal? Allowance { get; set; }

    public decimal? ContractIndemnity { get; set; }

    public decimal? Other { get; set; }

    public bool? ReturnHealthIns { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
