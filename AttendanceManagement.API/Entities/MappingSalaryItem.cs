using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class MappingSalaryItem
{
    public string Id { get; set; } = null!;

    public string? SalaryItemId { get; set; }
}
