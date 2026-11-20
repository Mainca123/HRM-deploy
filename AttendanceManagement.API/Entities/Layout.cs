using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Layout
{
    public string Item { get; set; } = null!;

    public byte Month { get; set; }

    public short Year { get; set; }

    public string Language { get; set; } = null!;

    public string? Value { get; set; }
}
