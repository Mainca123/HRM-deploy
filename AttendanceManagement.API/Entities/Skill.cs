using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Skill
{
    public int EmployeeId { get; set; }

    public string Duration { get; set; } = null!;

    public string? Organization { get; set; }

    public string? Terms { get; set; }

    public string? Diploma { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
