using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RegisterLateEarly
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public bool Type { get; set; }

    public int? Value { get; set; }

    public string? Reason { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
