using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Phone
{
    public int EmployeeId { get; set; }

    public string PhoneTypeId { get; set; } = null!;

    public string? Number { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual PhoneType PhoneType { get; set; } = null!;
}
