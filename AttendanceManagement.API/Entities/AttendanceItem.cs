using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AttendanceItem
{
    public string Id { get; set; } = null!;

    public string AttendanceItemGroupId { get; set; } = null!;

    public string? Name { get; set; }

    public string? DataType { get; set; }

    public string? Value { get; set; }

    public int? Rounding { get; set; }

    public byte? Type { get; set; }

    public bool? Display { get; set; }

    public bool? Status { get; set; }

    public int? SortOrder { get; set; }

    public virtual AttendanceItemGroup AttendanceItemGroup { get; set; } = null!;

    public virtual ICollection<RegisterAttendanceItem> RegisterAttendanceItems { get; set; } = new List<RegisterAttendanceItem>();
}
