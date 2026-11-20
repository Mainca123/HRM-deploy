using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class RightOfGroup
{
    public string GroupId { get; set; } = null!;

    public string FunctionId { get; set; } = null!;

    public byte FunctionType { get; set; }

    public virtual UserGroup Group { get; set; } = null!;
}
