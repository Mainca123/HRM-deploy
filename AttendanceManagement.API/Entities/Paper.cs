using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Paper
{
    public string EmployeeId { get; set; } = null!;

    public string PaperTypeId { get; set; } = null!;

    public bool? Available { get; set; }

    public int? Quantity { get; set; }

    public string? Notes { get; set; }
}
