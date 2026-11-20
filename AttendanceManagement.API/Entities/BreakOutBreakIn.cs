using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class BreakOutBreakIn
{
    public int EmployeeId { get; set; }

    public DateTime WorkingDay { get; set; }

    public string ShiftId { get; set; } = null!;

    public byte Iotimes { get; set; }

    public DateTime? BreakOut { get; set; }

    public DateTime? BreakIn { get; set; }

    public string? ReasonId { get; set; }

    public virtual TimeInTimeOut TimeInTimeOut { get; set; } = null!;
}
