using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/employees")]
[Produces("application/json")]
[Tags("Hồ sơ Nhân sự")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    /// <summary>
    /// Lấy danh sách nhân viên có phân trang, tìm kiếm theo tên/mã và lọc theo phòng ban
    /// </summary>
    /// <param name="pageIndex">Trang hiện tại (mặc định: 1)</param>
    /// <param name="pageSize">Số bản ghi trên trang (mặc định: 10, tối đa: 100)</param>
    /// <param name="search">Từ khóa tìm kiếm theo họ tên hoặc mã nhân viên</param>
    /// <param name="departmentId">Mã phòng ban cần lọc</param>
    [HttpGet]
    [Authorize(Roles = "1,4,ADMIN,HR,MANAGER")]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<EmployeeSummaryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? departmentId = null)
    {
        var result = await _employeeService.GetEmployeesPagedAsync(pageIndex, pageSize, search, departmentId);
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin chi tiết hồ sơ nhân viên theo ID
    /// </summary>
    /// <param name="id">Mã ID nhân viên</param>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var currentUsername = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Username");
        
        int? currentEmployeeId = null;
        var empIdClaim = User.FindFirstValue("EmployeeId");
        if (int.TryParse(empIdClaim, out var parsedId))
        {
            currentEmployeeId = parsedId;
        }

        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        var result = await _employeeService.GetEmployeeByIdAsync(id, currentUsername, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
            {
                return StatusCode(StatusCodes.Status403Forbidden, result);
            }
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Thêm mới nhân sự vào hệ thống (gán phòng ban và chức vụ ban đầu)
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequestDto request)
    {
        var result = await _employeeService.CreateEmployeeAsync(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Cập nhật thông tin lý lịch cá nhân và chuyển đổi phòng ban/chức vụ của nhân sự
    /// </summary>
    /// <param name="id">Mã ID nhân viên cần cập nhật</param>
    /// <param name="request">Thông tin cập nhật</param>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeRequestDto request)
    {
        var result = await _employeeService.UpdateEmployeeAsync(id, request);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Xóa hồ sơ nhân sự (Chỉ cho phép khi nhân viên chưa có dữ liệu chấm công / bảng lương)
    /// </summary>
    /// <param name="id">Mã ID nhân viên cần xóa</param>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "1,ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        return Ok(result);
    }
}
