using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class HealthStatus
{
    public int EmployeeId { get; set; }

    public int? Height { get; set; }

    public int? Weight { get; set; }

    public string? LeftVision { get; set; }

    public string? RightVision { get; set; }

    public string? BloodGroup { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
