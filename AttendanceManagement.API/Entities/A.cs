using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class A
{
    public string? Code { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? AbsenceId { get; set; }
}
