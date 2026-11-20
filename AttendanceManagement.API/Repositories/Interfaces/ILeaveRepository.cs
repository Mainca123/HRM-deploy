using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Leave;
using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu cho Nghỉ phép (Absence, RegisterAbsence, AnnualLeave)
/// </summary>
public interface ILeaveRepository
{
    /// <summary>
    /// Lấy danh mục toàn bộ loại nghỉ phép
    /// </summary>
    Task<List<Absence>> GetAbsenceTypesAsync();

    /// <summary>
    /// Kiểm tra mã loại nghỉ phép có tồn tại không
    /// </summary>
    Task<bool> AbsenceExistsAsync(string absenceId);

    /// <summary>
    /// Lấy danh sách đơn nghỉ phép có phân trang và lọc
    /// </summary>
    Task<PagedResultDto<LeaveRequestDto>> GetLeaveRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? status,
        DateTime? fromDate,
        DateTime? toDate);

    /// <summary>
    /// Lấy chi tiết 1 đơn nghỉ phép theo khóa chính ghép (EmployeeId, FromDate, AbsenceId)
    /// </summary>
    Task<LeaveRequestDto?> GetLeaveRequestDetailAsync(int employeeId, DateTime fromDate, string absenceId);

    /// <summary>
    /// Tìm thực thể RegisterAbsence theo khóa ghép để cập nhật trạng thái hoặc nội dung
    /// </summary>
    Task<RegisterAbsence?> GetLeaveRequestEntityAsync(int employeeId, DateTime fromDate, string absenceId);

    /// <summary>
    /// Kiểm tra xem nhân viên có đơn nghỉ phép nào (Chờ duyệt hoặc Đã duyệt) bị trùng khoảng thời gian không
    /// </summary>
    Task<bool> HasOverlappingLeaveAsync(
        int employeeId,
        DateTime fromDate,
        DateTime toDate,
        DateTime? excludeFromDate = null,
        string? excludeAbsenceId = null);

    /// <summary>
    /// Tạo mới đơn xin nghỉ phép
    /// </summary>
    Task CreateLeaveRequestAsync(RegisterAbsence entity);

    /// <summary>
    /// Cập nhật đơn xin nghỉ phép
    /// </summary>
    Task UpdateLeaveRequestAsync(RegisterAbsence entity);

    /// <summary>
    /// Xóa hẳn đơn nghỉ phép (dùng khi người tạo xóa trước khi duyệt)
    /// </summary>
    Task DeleteLeaveRequestAsync(RegisterAbsence entity);

    /// <summary>
    /// Tra cứu số dư phép năm của nhân viên theo năm
    /// </summary>
    Task<LeaveBalanceDto?> GetAnnualLeaveBalanceAsync(int employeeId, short year);
}

