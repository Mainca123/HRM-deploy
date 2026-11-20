using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Reward
{
    public int EmployeeId { get; set; }

    public string No { get; set; } = null!;

    public string? Content { get; set; }

    public string? Organization { get; set; }

    public string? Duration { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
