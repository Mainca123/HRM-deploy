using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class EmployeeInfoList
{
    public string FieldName { get; set; } = null!;

    public string? EmployeeInfoListGroupId { get; set; }

    public string? Caption { get; set; }

    public string? DataType { get; set; }

    public short? VisibleIndex { get; set; }

    public byte? Alignment { get; set; }
}
