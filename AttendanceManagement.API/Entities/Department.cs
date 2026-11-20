using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Department
{
    public string? ParentId { get; set; }

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Notes { get; set; }

    public short? SortOrder { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();

    public virtual ICollection<MonthAttendance> MonthAttendances { get; set; } = new List<MonthAttendance>();

    public virtual ICollection<MonthSalary> MonthSalaries { get; set; } = new List<MonthSalary>();

    public virtual ICollection<MonthWorking> MonthWorkings { get; set; } = new List<MonthWorking>();

    public virtual ICollection<SalaryTable> SalaryTables { get; set; } = new List<SalaryTable>();
}
