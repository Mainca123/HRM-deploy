using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Shift
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool? IsDefault { get; set; }

    public bool? IsNightShift { get; set; }

    public virtual ICollection<ShiftDetail> ShiftDetails { get; set; } = new List<ShiftDetail>();

    public virtual ICollection<TimeInTimeOut> TimeInTimeOuts { get; set; } = new List<TimeInTimeOut>();
}
