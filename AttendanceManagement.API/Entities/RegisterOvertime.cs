using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RegisterOvertime
{
    public int EmployeeId { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public byte Type { get; set; }

    public decimal? Value { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
