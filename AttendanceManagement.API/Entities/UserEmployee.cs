using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class UserEmployee
{
    public string UserName { get; set; } = null!;

    public int? EmployeeId { get; set; }

    public string? EmployeeIds { get; set; }
}
