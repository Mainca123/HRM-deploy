using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class EmployeePosition
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? PositionId { get; set; }

    public string? JobId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
