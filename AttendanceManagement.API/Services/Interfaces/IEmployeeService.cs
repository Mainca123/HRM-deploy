using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;

namespace AttendanceManagement.API.Services.Interfaces;

/// <summary>
/// Giao diện tầng nghiệp vụ cho hồ sơ nhân sự
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Lấy danh sách nhân viên có phân trang, tìm kiếm và lọc
    /// </summary>
    Task<ApiResponse<PagedResultDto<EmployeeSummaryDto>>> GetEmployeesPagedAsync(
        int pageIndex, 
        int pageSize, 
        string? search, 
        string? departmentId);

    /// <summary>
    /// Lấy chi tiết hồ sơ nhân viên (có kiểm tra phân quyền cho role Employee)
    /// </summary>
    Task<ApiResponse<EmployeeDetailDto>> GetEmployeeByIdAsync(
        int id, 
        string? currentUsername, 
        int? currentEmployeeId, 
        IList<string> roles);

    /// <summary>
    /// Thêm mới nhân sự vào hệ thống
    /// </summary>
    Task<ApiResponse<EmployeeDetailDto>> CreateEmployeeAsync(CreateEmployeeRequestDto request);

    /// <summary>
    /// Cập nhật thông tin lý lịch và phòng ban/chức vụ của nhân sự
    /// </summary>
    Task<ApiResponse<EmployeeDetailDto>> UpdateEmployeeAsync(int id, UpdateEmployeeRequestDto request);

    /// <summary>
    /// Xóa hồ sơ nhân sự (có kiểm tra ràng buộc toàn vẹn dữ liệu)
    /// </summary>
    Task<ApiResponse<bool>> DeleteEmployeeAsync(int id);
}

