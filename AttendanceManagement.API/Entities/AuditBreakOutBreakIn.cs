using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AuditBreakOutBreakIn
{
    public int EmployeeId { get; set; }

    public DateOnly WorkingDay { get; set; }

    public string ShiftId { get; set; } = null!;

    public byte Iotimes { get; set; }

    public DateTime? BreakOut { get; set; }

    public DateTime? BreakIn { get; set; }

    public string? ReasonId { get; set; }

    public virtual AuditTimeInTimeOut AuditTimeInTimeOut { get; set; } = null!;
}
