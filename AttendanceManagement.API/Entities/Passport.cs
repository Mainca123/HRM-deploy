using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Passport
{
    public int EmployeeId { get; set; }

    public string No { get; set; } = null!;

    public DateTime? IssuedDate { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public string? IssuedPlace { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
