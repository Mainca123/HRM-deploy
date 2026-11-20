using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AddressType
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
