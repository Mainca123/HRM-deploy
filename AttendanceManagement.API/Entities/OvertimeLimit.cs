using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class OvertimeLimit
{
    public string ShiftId { get; set; } = null!;

    public string ReplacementShiftId { get; set; } = null!;

    public DateOnly FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public decimal? Value { get; set; }

    public string? EmployeeIds { get; set; }
}
