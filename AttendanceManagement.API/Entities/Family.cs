using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Family
{
    public int EmployeeId { get; set; }

    public string FamilyTypeId { get; set; } = null!;

    public byte Ordinal { get; set; }

    public string? FullName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Occupation { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual FamilyType FamilyType { get; set; } = null!;
}
