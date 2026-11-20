using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class FingerprintTemplate
{
    public string EnrollNumber { get; set; } = null!;

    public byte Index { get; set; }

    public byte[]? Value { get; set; }
}
