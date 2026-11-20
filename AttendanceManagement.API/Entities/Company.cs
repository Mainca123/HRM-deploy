using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Company
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? CompanyTypeId { get; set; }

    public bool? Active { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public int? SortOrder { get; set; }

    public bool? Default { get; set; }

    public virtual CompanyType? CompanyType { get; set; }
}
