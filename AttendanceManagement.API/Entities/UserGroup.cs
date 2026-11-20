using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class UserGroup
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<RightOfGroup> RightOfGroups { get; set; } = new List<RightOfGroup>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
