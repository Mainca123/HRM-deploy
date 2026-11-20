using System.Security.Claims;
using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/attendance")]
[Produces("application/json")]
[Tags("Dữ liệu Chấm công & Ca làm việc (P2)")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
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
    /// P2.1: Danh sách các ca làm việc trong hệ thống kèm khung giờ chi tiết
    /// </summary>
    [HttpGet("shifts")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<ShiftDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetShifts()
    {
        var result = await _attendanceService.GetShiftsAsync();
        return Ok(result);
    }

    /// <summary>
    /// P2.2: Lịch sử quẹt thẻ vào/ra cá nhân của nhân viên đang đăng nhập theo tháng
    /// </summary>
    [HttpGet("my-history")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<TimeInTimeOutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMyAttendanceHistory([FromQuery] int? month = null, [FromQuery] int? year = null)
    {
        var (_, currentEmployeeId, _) = GetCurrentUserContext();
        var result = await _attendanceService.GetMyAttendanceHistoryAsync(currentEmployeeId, month, year);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.3: Tra cứu dữ liệu quẹt thẻ thực tế của toàn bộ nhân viên theo ngày và phòng ban
    /// </summary>
    [HttpGet("daily")]
    [Authorize(Roles = "1,2,4,6,8,ADMIN,HR,MANAGER,Administration")]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<TimeInTimeOutDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDailyAttendance(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] DateTime? date = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? search = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();
        var result = await _attendanceService.GetDailyAttendancePagedAsync(
            pageIndex, pageSize, date, departmentId, search, currentEmployeeId, roles);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.4: Ghi nhận Check-in trực tuyến (tự động gán ca mặc định HC nếu không chọn ca)
    /// </summary>
    [HttpPost("check-in")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<TimeInTimeOutDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<TimeInTimeOutDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckIn([FromBody] CheckInRequestDto request)
    {
        var (username, currentEmployeeId, _) = GetCurrentUserContext();
        var result = await _attendanceService.CheckInAsync(request, currentEmployeeId, username);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.5: Ghi nhận Check-out trực tuyến ngày hôm nay
    /// </summary>
    [HttpPost("check-out")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<TimeInTimeOutDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<TimeInTimeOutDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckOut([FromBody] CheckOutRequestDto request)
    {
        var (username, currentEmployeeId, _) = GetCurrentUserContext();
        var result = await _attendanceService.CheckOutAsync(request, currentEmployeeId, username);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.6: Danh sách đơn giải trình đi muộn / về sớm
    /// </summary>
    [HttpGet("explain-late-early")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<RegisterLateEarlyDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLateEarlyRequests(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? employeeId = null,
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();
        var result = await _attendanceService.GetLateEarlyRequestsPagedAsync(
            pageIndex, pageSize, employeeId, fromDate, toDate, currentEmployeeId, roles);

        return Ok(result);
    }

    /// <summary>
    /// P2.7: Gửi đơn giải trình đi muộn / về sớm mới
    /// </summary>
    [HttpPost("explain-late-early")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<RegisterLateEarlyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RegisterLateEarlyDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateLateEarlyRequest([FromBody] CreateLateEarlyRequestDto request)
    {
        var (username, currentEmployeeId, _) = GetCurrentUserContext();
        var result = await _attendanceService.CreateLateEarlyRequestAsync(request, currentEmployeeId, username);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.8: Phê duyệt giải trình đi muộn / về sớm (Chỉ dành cho Admin - Nhóm 1)
    /// </summary>
    [HttpPut("explain-late-early/approve")]
    [Authorize(Roles = "1,ADMIN,Administration")]
    [ProducesResponseType(typeof(ApiResponse<RegisterLateEarlyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<RegisterLateEarlyDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<RegisterLateEarlyDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveLateEarlyRequest([FromBody] ApproveLateEarlyRequestDto request)
    {
        var (username, _, roles) = GetCurrentUserContext();
        var result = await _attendanceService.ApproveLateEarlyRequestAsync(request, username, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.9: Xóa/hủy đơn giải trình đi muộn / về sớm
    /// </summary>
    [HttpDelete("explain-late-early")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteLateEarlyRequest(
        [FromQuery] int employeeId,
        [FromQuery] DateTime fromDate,
        [FromQuery] bool type)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();
        var result = await _attendanceService.DeleteLateEarlyRequestAsync(employeeId, fromDate, type, currentEmployeeId, roles);
        if (!result.Success)
        {
            if (result.Message.Contains("Không tìm thấy", StringComparison.OrdinalIgnoreCase))
                return NotFound(result);
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P2.10: Bảng tổng hợp công tháng xoay ngang (Tổng công NC, Tổng giờ tăng ca OT1..OT6, Đi muộn, Tiền ăn)
    /// </summary>
    [HttpGet("monthly-summary")]
    [Authorize(Roles = "1,2,4,6,8,ADMIN,HR,MANAGER,Administration")]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<MonthlyAttendanceSummaryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMonthlyAttendanceSummary(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] int? month = null,
        [FromQuery] int? year = null,
        [FromQuery] string? departmentId = null,
        [FromQuery] string? search = null)
    {
        var (_, _, roles) = GetCurrentUserContext();
        var result = await _attendanceService.GetMonthlyAttendanceSummaryPagedAsync(
            pageIndex, pageSize, month, year, departmentId, search, roles);

        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}

