using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Employee1
{
    public int Spid { get; set; }

    public int Id { get; set; }

    public string? Code { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public short? SortOrder { get; set; }

    public DateTime WorkingDay { get; set; }

    public string? PositionName { get; set; }
}
