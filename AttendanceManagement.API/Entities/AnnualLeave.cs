using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AnnualLeave
{
    public int EmployeeId { get; set; }

    public byte Month { get; set; }

    public short Year { get; set; }

    public decimal? Value { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
