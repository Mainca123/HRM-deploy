using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Absence
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public bool? Company { get; set; }

    public bool? Si { get; set; }

    public virtual ICollection<RegisterAbsence> RegisterAbsences { get; set; } = new List<RegisterAbsence>();
}
