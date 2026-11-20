using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RegisterAbsence
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? Unit { get; set; }

    public string AbsenceId { get; set; } = null!;

    public string? Notes { get; set; }

    public byte? Status { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? ApprovedUserName { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public virtual Absence Absence { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
