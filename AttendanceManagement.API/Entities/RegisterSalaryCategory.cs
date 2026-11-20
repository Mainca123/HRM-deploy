using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RegisterSalaryCategory
{
    public int EmployeeId { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? SalaryCategoryId { get; set; }

    public string? CreatedUserName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? ModifiedUserName { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
