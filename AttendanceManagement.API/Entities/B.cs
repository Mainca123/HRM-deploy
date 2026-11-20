using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class B
{
    public string? Code { get; set; }

    public string? Id { get; set; }

    public DateOnly? FromDate { get; set; }

    public DateOnly? ToDate { get; set; }

    public string? AbsenceId { get; set; }

    public decimal? Type { get; set; }
}
