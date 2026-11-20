using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Vacation
{
    public DateTime Id { get; set; }

    public bool? HasWages { get; set; }

    public string? Notes { get; set; }

    public string? EmployeeIds { get; set; }
}
