using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IOrganizationRepository organizationRepository)
    {
        _employeeRepository = employeeRepository;
        _organizationRepository = organizationRepository;
    }

    public async Task<ApiResponse<PagedResultDto<EmployeeSummaryDto>>> GetEmployeesPagedAsync(
        int pageIndex,
        int pageSize,
        string? search,
        string? departmentId)
    {
        // Chuẩn hóa tham số phân trang
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var result = await _employeeRepository.GetEmployeesPagedAsync(pageIndex, pageSize, search, departmentId);
        return ApiResponse<PagedResultDto<EmployeeSummaryDto>>.Ok(result, "Lấy danh sách nhân viên thành công.");
    }

    public async Task<ApiResponse<EmployeeDetailDto>> GetEmployeeByIdAsync(
        int id,
        string? currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        // Kiểm tra phân quyền: Nếu là nhân viên thông thường, chỉ được xem hồ sơ chính mình
        bool isPrivileged = roles.Any(r => r is "1" or "4" or "ADMIN" or "HR" or "MANAGER");
        if (!isPrivileged)
        {
            if (!currentEmployeeId.HasValue || currentEmployeeId.Value != id)
            {
                return ApiResponse<EmployeeDetailDto>.Fail("Bạn không có quyền xem hồ sơ của nhân viên khác.");
            }
        }

        var employee = await _employeeRepository.GetEmployeeDetailByIdAsync(id);
        if (employee == null)
        {
            return ApiResponse<EmployeeDetailDto>.Fail("Không tìm thấy nhân viên với ID đã cung cấp.");
        }

        return ApiResponse<EmployeeDetailDto>.Ok(employee, "Lấy thông tin chi tiết nhân viên thành công.");
    }

    public async Task<ApiResponse<EmployeeDetailDto>> CreateEmployeeAsync(CreateEmployeeRequestDto request)
    {
        // 1. Kiểm tra trùng lặp mã nhân viên
        var codeTrimmed = request.Code.Trim();
        if (await _employeeRepository.ExistsByCodeAsync(codeTrimmed))
        {
            return ApiResponse<EmployeeDetailDto>.Fail($"Mã nhân viên '{codeTrimmed}' đã tồn tại trong hệ thống.");
        }

        // 2. Kiểm tra sự tồn tại của phòng ban
        if (!await _organizationRepository.DepartmentExistsAsync(request.DepartmentId))
        {
            return ApiResponse<EmployeeDetailDto>.Fail($"Phòng ban với mã '{request.DepartmentId}' không tồn tại.");
        }

        // 3. Kiểm tra sự tồn tại của chức vụ
        if (!await _organizationRepository.PositionExistsAsync(request.PositionId))
        {
            return ApiResponse<EmployeeDetailDto>.Fail($"Chức vụ với mã '{request.PositionId}' không tồn tại.");
        }

        // 4. Lấy ID kế tiếp an toàn
        var nextId = await _employeeRepository.GetNextIdAsync();

        // 5. Khởi tạo đối tượng nhân viên
        var employee = new Employee
        {
            Id = nextId,
            Code = codeTrimmed,
            FullName = request.FullName.Trim(),
            BirthDate = request.BirthDate,
            Sex = request.Sex,
            Email = request.Email?.Trim(),
            Notes = request.Notes?.Trim()
        };

        var joinDate = request.JoinDate ?? DateTime.Today;

        // 6. Thêm mới nhân viên kèm phòng ban & chức vụ trong Transaction
        await _employeeRepository.CreateEmployeeWithOrgAsync(
            employee,
            request.DepartmentId,
            request.PositionId,
            joinDate);

        // 7. Lấy lại thông tin chi tiết vừa tạo để trả về
        var createdDto = await _employeeRepository.GetEmployeeDetailByIdAsync(nextId);
        return ApiResponse<EmployeeDetailDto>.Ok(createdDto!, "Thêm mới nhân viên thành công.");
    }

    public async Task<ApiResponse<EmployeeDetailDto>> UpdateEmployeeAsync(int id, UpdateEmployeeRequestDto request)
    {
        // 1. Tìm nhân viên cần sửa
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return ApiResponse<EmployeeDetailDto>.Fail("Không tìm thấy nhân viên cần cập nhật.");
        }

        // 2. Kiểm tra trùng mã nhân viên với người khác
        var codeTrimmed = request.Code.Trim();
        if (await _employeeRepository.ExistsByCodeAsync(codeTrimmed, id))
        {
            return ApiResponse<EmployeeDetailDto>.Fail($"Mã nhân viên '{codeTrimmed}' đã được sử dụng bởi nhân viên khác.");
        }

        // 3. Kiểm tra phòng ban mới nếu có chọn
        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
        {
            if (!await _organizationRepository.DepartmentExistsAsync(request.DepartmentId))
            {
                return ApiResponse<EmployeeDetailDto>.Fail($"Phòng ban với mã '{request.DepartmentId}' không tồn tại.");
            }
        }

        // 4. Kiểm tra chức vụ mới nếu có chọn
        if (!string.IsNullOrWhiteSpace(request.PositionId))
        {
            if (!await _organizationRepository.PositionExistsAsync(request.PositionId))
            {
                return ApiResponse<EmployeeDetailDto>.Fail($"Chức vụ với mã '{request.PositionId}' không tồn tại.");
            }
        }

        // 5. Cập nhật các trường thông tin cơ bản
        employee.Code = codeTrimmed;
        employee.FullName = request.FullName.Trim();
        employee.BirthDate = request.BirthDate;
        employee.Sex = request.Sex;
        employee.Email = request.Email?.Trim();
        employee.Notes = request.Notes?.Trim();

        // 6. Lưu và cập nhật lịch sử phòng ban / chức vụ
        await _employeeRepository.UpdateEmployeeWithOrgAsync(
            employee,
            request.DepartmentId,
            request.PositionId);

        // 7. Lấy thông tin sau cập nhật
        var updatedDto = await _employeeRepository.GetEmployeeDetailByIdAsync(id);
        return ApiResponse<EmployeeDetailDto>.Ok(updatedDto!, "Cập nhật hồ sơ nhân viên thành công.");
    }

    public async Task<ApiResponse<bool>> DeleteEmployeeAsync(int id)
    {
        // 1. Kiểm tra sự tồn tại của nhân viên
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee == null)
        {
            return ApiResponse<bool>.Fail("Không tìm thấy nhân viên cần xóa.");
        }

        // 2. Kiểm tra ràng buộc toàn vẹn dữ liệu phát sinh
        if (await _employeeRepository.HasDependentDataAsync(id))
        {
            return ApiResponse<bool>.Fail(
                "Không thể xóa nhân viên này vì đã có dữ liệu phát sinh (chấm công, bảng lương hoặc đơn từ). Vui lòng chuyển trạng thái công tác thay vì xóa hoàn toàn.");
        }

        // 3. Thực hiện xóa an toàn
        await _employeeRepository.DeleteEmployeeAsync(id);
        return ApiResponse<bool>.Ok(true, "Xóa hồ sơ nhân viên thành công.");
    }
}

