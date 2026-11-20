using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Religion
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
