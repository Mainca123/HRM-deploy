using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class SpecialDay
{
    public DateOnly WorkingDay { get; set; }

    public byte Type { get; set; }
}
