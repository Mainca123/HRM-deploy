using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class TimeInTimeOut1
{
    public int Spid { get; set; }

    public int EmployeeId { get; set; }

    public DateTime WorkingDay { get; set; }

    public string ShiftId { get; set; } = null!;

    public DateTime? TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public byte? Status { get; set; }

    public string? Notes { get; set; }
}
