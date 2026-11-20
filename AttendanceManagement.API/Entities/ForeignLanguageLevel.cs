using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class ForeignLanguageLevel
{
    public int EmployeeId { get; set; }

    public string ForeignLanguageId { get; set; } = null!;

    public string? Level { get; set; }

    public string? Status { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
