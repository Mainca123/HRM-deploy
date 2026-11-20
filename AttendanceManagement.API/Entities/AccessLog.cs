using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class AccessLog
{
    public DateTime WorkingDay { get; set; }

    public string UserName { get; set; } = null!;

    public string? Content { get; set; }

    public string? Macaddress { get; set; }
}
