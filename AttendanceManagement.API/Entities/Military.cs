using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Military
{
    public int EmployeeId { get; set; }

    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Reason { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
