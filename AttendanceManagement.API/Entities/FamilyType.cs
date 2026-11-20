using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class FamilyType
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Family> Families { get; set; } = new List<Family>();
}
