using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AuditTimeInTimeOut
{
    public int EmployeeId { get; set; }

    public DateOnly WorkingDay { get; set; }

    public string ShiftId { get; set; } = null!;

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<AuditBreakOutBreakIn> AuditBreakOutBreakIns { get; set; } = new List<AuditBreakOutBreakIn>();

    public virtual Employee Employee { get; set; } = null!;
}
