using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class CompanyType
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? NameEn { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
