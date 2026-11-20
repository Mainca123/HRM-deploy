using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Title
{
    public string Item { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string? Value { get; set; }

    public string? Description { get; set; }
}
