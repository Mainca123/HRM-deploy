using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class WorkingTimeRule
{
    public DateTime FromTimeIn { get; set; }

    public DateTime ToTimeIn { get; set; }

    public DateTime FromTimeOut { get; set; }

    public DateTime ToTimeOut { get; set; }

    public string? ShiftId { get; set; }
}
