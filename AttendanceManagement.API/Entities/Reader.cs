using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Reader
{
    public byte Id { get; set; }

    public string? ReaderTypeId { get; set; }

    public string? CommType { get; set; }

    public string? Ipaddress { get; set; }

    public string? CommPort { get; set; }

    public int? CommPassword { get; set; }

    public DateTime? LastTime { get; set; }

    public int? Period { get; set; }

    public DateTime? LastRecord { get; set; }

    public string? Description { get; set; }

    public bool? In { get; set; }

    public bool? Used { get; set; }
}
