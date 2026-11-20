using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Health
{
    public int EmployeeId { get; set; }

    public int? Weight { get; set; }

    public int? Height { get; set; }

    public string? BloodGroup { get; set; }

    public string? Allergy { get; set; }

    public string? Disease { get; set; }

    public string? Defect { get; set; }

    public string? Vaccination { get; set; }

    public string? Other { get; set; }

    public int? HealthStatusId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
