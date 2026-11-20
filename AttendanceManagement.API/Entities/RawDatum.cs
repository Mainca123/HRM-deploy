using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RawDatum
{
    public string? ReaderId { get; set; }

    public string EnrollNumber { get; set; } = null!;

    public DateTime SwipedDate { get; set; }

    public bool? Status { get; set; }

    public bool? Used { get; set; }
}
