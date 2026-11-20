using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Datum
{
    public double? Idm { get; set; }

    public string? Idcard { get; set; }

    public DateTime? ThoiGian { get; set; }

    public bool Status { get; set; }

    public bool HandData { get; set; }

    public bool Pass { get; set; }
}
