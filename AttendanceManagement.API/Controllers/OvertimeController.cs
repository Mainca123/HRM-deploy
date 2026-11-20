using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Overtime;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/overtime")]
[Produces("application/json")]
[Tags("Quản lý Làm thêm giờ (Overtime)")]
public class OvertimeController : ControllerBase
{
    private readonly IOvertimeService _overtimeService;

    public OvertimeController(IOvertimeService overtimeService)
    {
        _overtimeService = overtimeService;
    }

    private (string Username, int? EmployeeId, List<string> Roles) GetCurrentUserContext()
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Username") ?? string.Empty;

        int? empId = null;
        if (int.TryParse(User.FindFirstValue("EmployeeId"), out var parsedId))
        {
            empId = parsedId;
        }

        var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        return (username, empId, roles);
    }

    /// <summary>
    /// Lấy danh mục các loại tăng ca và cấu hình giới hạn tăng ca (OvertimeLimit)
    /// </summary>
    [HttpGet("types")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OvertimeTypesResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOvertimeTypes()
    {
        var result = await _overtimeService.GetOvertimeTypesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách đăng ký làm thêm giờ có phân trang và lọc (Admin xem tất cả; các nhóm khác chỉ xem của mình)
    /// </summary>
    [HttpGet("requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<OvertimeRequestDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOvertimeRequests(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? employeeId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] byte? type = null,
        [FromQuery] DateOnly? fromDate = null,
        [FromQuery] DateOnly? toDate = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _overtimeService.GetOvertimeRequestsPagedAsync(
            pageIndex, pageSize, employeeId, departmentId, type, fromDate, toDate, currentEmployeeId, roles);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Xem chi tiết một bản ghi đăng ký làm thêm giờ theo khóa chính ghép (EmployeeId, FromDate, Type)
    /// </summary>
    [HttpGet("requests/detail")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOvertimeRequestDetail(
        [FromQuery] int employeeId,
        [FromQuery] DateOnly fromDate,
        [FromQuery] byte type)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _overtimeService.GetOvertimeRequestDetailAsync(
            employeeId, fromDate, type, currentEmployeeId, roles);

        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Đăng ký làm thêm giờ mới
    /// </summary>
    [HttpPost("requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateOvertimeRequest([FromBody] CreateOvertimeRequestDto request)
    {
        var (username, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _overtimeService.CreateOvertimeRequestAsync(request, username, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Cập nhật / Xác nhận bản ghi đăng ký làm thêm giờ (Ghi nhận ModifiedUserName và ModifiedDate)
    /// </summary>
    [HttpPut("requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<OvertimeRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOvertimeRequest([FromBody] UpdateOvertimeRequestDto request)
    {
        var (username, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _overtimeService.UpdateOvertimeRequestAsync(request, username, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Xóa / Hủy bản ghi đăng ký làm thêm giờ
    /// </summary>
    [HttpDelete("requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOvertimeRequest(
        [FromQuery] int employeeId,
        [FromQuery] DateOnly fromDate,
        [FromQuery] byte type)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _overtimeService.DeleteOvertimeRequestAsync(
            employeeId, fromDate, type, currentEmployeeId, roles);

        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }

        return Ok(result);
    }
}

