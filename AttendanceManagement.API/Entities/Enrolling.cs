using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Enrolling
{
    public int EmployeeId { get; set; }

    public DateTime? UnionEnrollingDate { get; set; }

    public string? UnionEnrollingPlace { get; set; }

    public DateTime? PartyEnrollingDate { get; set; }

    public string? PartyEnrollingPlace { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
