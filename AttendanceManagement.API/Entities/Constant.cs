using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Constant
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public string? Description { get; set; }
}
