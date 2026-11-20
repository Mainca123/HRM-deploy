using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SalaryCode
{
    public string Id { get; set; } = null!;

    public decimal? Value { get; set; }
}
