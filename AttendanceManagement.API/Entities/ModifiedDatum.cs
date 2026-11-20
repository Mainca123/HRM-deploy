using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class ModifiedDatum
{
    public int EmployeeId { get; set; }

    public DateTime SwipedDate { get; set; }

    public DateTime? Value { get; set; }

    public byte? Status { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
