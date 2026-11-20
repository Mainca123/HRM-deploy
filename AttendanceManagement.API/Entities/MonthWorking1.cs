using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class MonthWorking1
{
    public int Spid { get; set; }

    public int EmployeeId { get; set; }

    public string? DepartmentId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public decimal? Value { get; set; }
}
