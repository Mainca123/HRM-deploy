using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Card
{
    public int EmployeeId { get; set; }

    public string? Id { get; set; }

    public string? EnrollNumber { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
