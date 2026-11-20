using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Entities;

namespace AttendanceManagement.API.Repositories.Interfaces;

/// <summary>
/// Giao diện tầng truy xuất dữ liệu nhân viên và lịch sử công tác
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// Lấy danh sách nhân viên có phân trang, lọc theo phòng ban và tìm kiếm
    /// </summary>
    Task<PagedResultDto<EmployeeSummaryDto>> GetEmployeesPagedAsync(
        int pageIndex, 
        int pageSize, 
        string? search, 
        string? departmentId);

    /// <summary>
    /// Lấy chi tiết hồ sơ nhân viên (kèm phòng ban, chức vụ hiện tại và tài khoản)
    /// </summary>
    Task<EmployeeDetailDto?> GetEmployeeDetailByIdAsync(int id);

    /// <summary>
    /// Tìm thực thể nhân viên theo ID để phục vụ cập nhật
    /// </summary>
    Task<Employee?> GetByIdAsync(int id);

    /// <summary>
    /// Tự động sinh ID nhân viên kế tiếp (Max(Id) + 1)
    /// </summary>
    Task<int> GetNextIdAsync();

    /// <summary>
    /// Kiểm tra mã nhân viên (Code) đã tồn tại trong hệ thống chưa
    /// </summary>
    /// <param name="code">Mã cần kiểm tra</param>
    /// <param name="excludeId">Bỏ qua ID này khi kiểm tra (dùng cho trường hợp update)</param>
    Task<bool> ExistsByCodeAsync(string code, int? excludeId = null);

    /// <summary>
    /// Thêm mới nhân viên kèm phòng ban và chức vụ khởi đầu trong một Transaction
    /// </summary>
    Task CreateEmployeeWithOrgAsync(
        Employee employee, 
        string departmentId, 
        string positionId, 
        DateTime joinDate);

    /// <summary>
    /// Cập nhật thông tin nhân viên và điều chuyển phòng ban / chức vụ nếu có thay đổi
    /// </summary>
    Task UpdateEmployeeWithOrgAsync(
        Employee employee, 
        string? newDepartmentId, 
        string? newPositionId);

    /// <summary>
    /// Kiểm tra xem nhân viên đã có dữ liệu phát sinh (chấm công, bảng lương, nghỉ phép, tăng ca) chưa
    /// </summary>
    Task<bool> HasDependentDataAsync(int id);

    /// <summary>
    /// Xóa hoàn toàn hồ sơ nhân viên và lịch sử phòng ban/chức vụ/tài khoản liên kết
    /// </summary>
    Task DeleteEmployeeAsync(int id);
}

