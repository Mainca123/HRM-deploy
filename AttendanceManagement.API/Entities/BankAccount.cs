using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class BankAccount
{
    public int EmployeeId { get; set; }

    public string BankId { get; set; } = null!;

    public string? AccountNumber { get; set; }

    public string? Note { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
