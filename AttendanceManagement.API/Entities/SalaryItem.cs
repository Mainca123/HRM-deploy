using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SalaryItem
{
    public string Id { get; set; } = null!;

    public string SalaryItemGroupId { get; set; } = null!;

    public string? Name { get; set; }

    public string? DataType { get; set; }

    public string? Value { get; set; }

    public int? Rounding { get; set; }

    public byte? Type { get; set; }

    public string? Display { get; set; }

    public bool? Status { get; set; }

    public int? SortOrder { get; set; }

    public virtual SalaryItemGroup SalaryItemGroup { get; set; } = null!;
}
