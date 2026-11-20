using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SalaryItemGroup
{
    public string? ParentId { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<SalaryItem> SalaryItems { get; set; } = new List<SalaryItem>();
}
