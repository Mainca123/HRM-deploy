using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class User
{
    public string Name { get; set; } = null!;

    public string? Password { get; set; }

    public string? TypeId { get; set; }

    public bool? Permission { get; set; }

    public byte? AuditData { get; set; }

    public string? GroupId { get; set; }

    public virtual UserGroup? Group { get; set; }
}
