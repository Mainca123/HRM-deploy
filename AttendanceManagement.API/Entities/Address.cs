using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Address
{
    public int EmployeeId { get; set; }

    public string AddressTypeId { get; set; } = null!;

    public string? Name { get; set; }

    public virtual AddressType AddressType { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
