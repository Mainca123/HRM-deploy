using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class HealthInsuranceCard
{
    public int EmployeeId { get; set; }

    public string? Number { get; set; }

    public string? ProvinceId { get; set; }

    public string? HospitalId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
