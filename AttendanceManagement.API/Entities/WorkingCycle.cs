using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class WorkingCycle
{
    public string DepartmentId { get; set; } = null!;

    public byte Month { get; set; }

    public short Year { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }
}
