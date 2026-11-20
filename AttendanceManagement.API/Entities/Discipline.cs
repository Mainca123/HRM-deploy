using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Discipline
{
    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public int No { get; set; }

    public string? Content { get; set; }

    public string? Form { get; set; }

    public DateTime? Duration { get; set; }

    public string? Notes { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
