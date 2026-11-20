namespace AttendanceManagement.API.Enums;

/// <summary>
/// Định nghĩa các mã nhóm quyền chuẩn theo dữ liệu gốc của bảng UserGroup
/// </summary>
public static class AppRoles
{
    /// <summary>
    /// ID = 1: Administration - Quản trị toàn hệ thống
    /// </summary>
    public const string Administration = "1";

    /// <summary>
    /// ID = 2: Chấm Công - Phụ trách máy chấm công, vào ra
    /// </summary>
    public const string Attendance = "2";

    /// <summary>
    /// ID = 3: Tiền lương - Phụ trách bảng lương
    /// </summary>
    public const string Payroll = "3";

    /// <summary>
    /// ID = 4: Nhân sự - Quản lý hồ sơ nhân viên (HR)
    /// </summary>
    public const string HumanResources = "4";

    /// <summary>
    /// ID = 5: Nhà ăn - Quản lý suất ăn ca
    /// </summary>
    public const string Catering = "5";

    /// <summary>
    /// ID = 6: Xí nghiệp 1 - Quản lý phân xưởng 1
    /// </summary>
    public const string Enterprise1 = "6";

    /// <summary>
    /// ID = 8: Xí nghiệp 3 - Quản lý phân xưởng 3
    /// </summary>
    public const string Enterprise3 = "8";
}

