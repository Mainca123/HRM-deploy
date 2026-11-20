using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class OtherLevel
{
    public int EmployeeId { get; set; }

    public string Category { get; set; } = null!;

    public string? Details { get; set; }
}
