using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AttendanceTable
{
    public string Id { get; set; } = null!;

    public string AttendanceTableGroupId { get; set; } = null!;

    public string? Name { get; set; }

    public string? DataType { get; set; }

    public string? Value { get; set; }

    public byte? Type { get; set; }

    public string? Position { get; set; }

    public string? Display { get; set; }

    public int? SortOrder { get; set; }

    public virtual AttendanceTableGroup AttendanceTableGroup { get; set; } = null!;
}
