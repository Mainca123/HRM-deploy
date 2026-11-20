using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class CommandButton
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Owner { get; set; }

    public string? Text { get; set; }
}
