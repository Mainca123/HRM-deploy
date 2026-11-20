using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class ProfessionalLevel
{
    public int EmployeeId { get; set; }

    public int Order { get; set; }

    public string? Name { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
