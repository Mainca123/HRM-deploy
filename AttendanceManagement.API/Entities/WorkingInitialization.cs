using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class WorkingInitialization
{
    public int EmployeeId { get; set; }

    public DateTime? InitialDate { get; set; }

    public DateTime? SigningDate { get; set; }

    public string? BySource { get; set; }

    public string? PresentPerson { get; set; }

    public string? Interviewer { get; set; }

    public string? RecruitingOfficer { get; set; }

    public bool? HasHealthIns { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
