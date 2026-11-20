using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class PhoneType
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
}
