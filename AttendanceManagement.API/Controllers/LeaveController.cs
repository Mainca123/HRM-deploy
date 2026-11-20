using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Leave;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Produces("application/json")]
[Tags("Quản lý Nghỉ phép")]
public class LeaveController : ControllerBase
{
    private readonly ILeaveService _leaveService;

    public LeaveController(ILeaveService leaveService)
    {
        _leaveService = leaveService;
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
    /// Lấy danh mục toàn bộ loại nghỉ phép (Phép năm, Ốm đau, Thai sản, Không lương...)
    /// </summary>
    [HttpGet("api/absence-types")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<AbsenceTypeDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAbsenceTypes()
    {
        var result = await _leaveService.GetAbsenceTypesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Lấy danh sách đơn xin nghỉ phép có phân trang và bộ lọc (Admin xem tất cả; các nhóm khác chỉ xem đơn của mình)
    /// </summary>
    [HttpGet("api/leave-requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<LeaveRequestDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLeaveRequests(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? employeeId = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] byte? status = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.GetLeaveRequestsPagedAsync(
            pageIndex, pageSize, employeeId, departmentId, status, fromDate, toDate, currentEmployeeId, roles);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Xem chi tiết một đơn xin nghỉ phép theo khóa chính ghép (EmployeeId, FromDate, AbsenceId)
    /// </summary>
    [HttpGet("api/leave-requests/detail")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLeaveRequestDetail(
        [FromQuery] int employeeId,
        [FromQuery] DateTime fromDate,
        [FromQuery] string absenceId)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.GetLeaveRequestDetailAsync(
            employeeId, fromDate, absenceId, currentEmployeeId, roles);

        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Tạo mới đơn xin nghỉ phép (Mặc định Status = 0: Chờ duyệt)
    /// </summary>
    [HttpPost("api/leave-requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateLeaveRequest([FromBody] CreateLeaveRequestDto request)
    {
        var (username, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.CreateLeaveRequestAsync(request, username, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            return BadRequest(result);
        }

        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Chỉnh sửa đơn xin nghỉ phép (Chỉ cho phép khi đơn đang ở trạng thái Chờ duyệt)
    /// </summary>
    [HttpPut("api/leave-requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLeaveRequest([FromBody] UpdateLeaveRequestDto request)
    {
        var (username, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.UpdateLeaveRequestAsync(request, username, currentEmployeeId, roles);
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
    /// Phê duyệt đơn xin nghỉ phép (Chỉ dành cho Admin - Nhóm 1)
    /// </summary>
    [HttpPost("api/leave-requests/approve")]
    [Authorize(Roles = "1,ADMIN,Administration")]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveLeaveRequest([FromBody] LeaveRequestKeyDto request)
    {
        var (username, _, roles) = GetCurrentUserContext();

        var result = await _leaveService.ApproveLeaveRequestAsync(request, username, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Từ chối đơn xin nghỉ phép kèm lý do (Chỉ dành cho Admin - Nhóm 1)
    /// </summary>
    [HttpPost("api/leave-requests/reject")]
    [Authorize(Roles = "1,ADMIN,Administration")]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RejectLeaveRequest([FromBody] RejectLeaveRequestDto request)
    {
        var (username, _, roles) = GetCurrentUserContext();

        var result = await _leaveService.RejectLeaveRequestAsync(request, username, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Hủy đơn xin nghỉ phép (Chuyển trạng thái sang Status = 3; nhân viên chỉ hủy được khi chưa duyệt)
    /// </summary>
    [HttpPost("api/leave-requests/cancel")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<LeaveRequestDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelLeaveRequest([FromBody] LeaveRequestKeyDto request)
    {
        var (username, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.CancelLeaveRequestAsync(request, username, currentEmployeeId, roles);
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
    /// Xóa đơn xin nghỉ phép khỏi hệ thống (Nhân viên chỉ xóa được đơn của mình khi chưa được duyệt)
    /// </summary>
    [HttpDelete("api/leave-requests")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLeaveRequest(
        [FromQuery] int employeeId,
        [FromQuery] DateTime fromDate,
        [FromQuery] string absenceId)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.DeleteLeaveRequestAsync(
            employeeId, fromDate, absenceId, currentEmployeeId, roles);

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
    /// Tra cứu số dư phép năm còn lại của nhân viên
    /// </summary>
    [HttpGet("api/leave-requests/balance/{employeeId:int}")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<LeaveBalanceDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<LeaveBalanceDto>), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ApiResponse<LeaveBalanceDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLeaveBalance(int employeeId, [FromQuery] short? year = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();

        var result = await _leaveService.GetAnnualLeaveBalanceAsync(employeeId, year, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("không có quyền", StringComparison.OrdinalIgnoreCase))
                return StatusCode(StatusCodes.Status403Forbidden, result);
            return NotFound(result);
        }

        return Ok(result);
    }
}

