using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SocialInsuranceBook
{
    public int EmployeeId { get; set; }

    public string? Value { get; set; }

    public DateTime? DateOfIssue { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
