using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class PaidAbsence
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? SortOrder { get; set; }
}
