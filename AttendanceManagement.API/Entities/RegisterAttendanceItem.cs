using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RegisterAttendanceItem
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string AttendanceItemId { get; set; } = null!;

    public decimal? Value { get; set; }

    public string? Text { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual AttendanceItem AttendanceItem { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
