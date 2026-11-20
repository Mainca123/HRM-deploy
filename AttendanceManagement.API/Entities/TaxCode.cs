using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class TaxCode
{
    public int EmployeeId { get; set; }

    public string? Code { get; set; }

    public DateTime? DateOfIssue { get; set; }

    public string? FirstDependent { get; set; }

    public string? SecondDependent { get; set; }
}
