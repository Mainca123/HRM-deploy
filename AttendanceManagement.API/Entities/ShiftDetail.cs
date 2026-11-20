using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class ShiftDetail
{
    public string ShiftId { get; set; } = null!;

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public DateTime? NormalTimeIn { get; set; }

    public DateTime? NormalBreakOut { get; set; }

    public DateTime? NormalBreakIn { get; set; }

    public DateTime? NormalTimeOut { get; set; }

    public int? NormalTotal { get; set; }

    public DateTime? SaturdayTimeIn { get; set; }

    public DateTime? SaturdayBreakOut { get; set; }

    public DateTime? SaturdayBreakIn { get; set; }

    public DateTime? SaturdayTimeOut { get; set; }

    public int? SaturdayTotal { get; set; }

    public DateTime? SundayTimeIn { get; set; }

    public DateTime? SundayBreakOut { get; set; }

    public DateTime? SundayBreakIn { get; set; }

    public DateTime? SundayTimeOut { get; set; }

    public int? SundayTotal { get; set; }

    public int? LateComing { get; set; }

    public int? EarlyReturning { get; set; }

    public int? PreviousOvertime { get; set; }

    public int? NextOvertime { get; set; }

    public virtual Shift Shift { get; set; } = null!;
}
