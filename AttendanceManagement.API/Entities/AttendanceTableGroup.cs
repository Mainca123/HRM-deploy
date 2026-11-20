using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AttendanceTableGroup
{
    public string? ParentId { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public int? SortOrder { get; set; }

    public virtual ICollection<AttendanceTable> AttendanceTables { get; set; } = new List<AttendanceTable>();
}
