using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class BeforeWorking
{
    public int EmployeeId { get; set; }

    public string Organization { get; set; } = null!;

    public string? Duration { get; set; }

    public string? JobTitle { get; set; }

    public string? Result { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
