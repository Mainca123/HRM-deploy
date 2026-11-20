using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/dashboard")]
[Produces("application/json")]
[Tags("Dashboard Thống kê (P3)")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
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
    /// P3.1: Dashboard cá nhân Nhân viên (Ngày công, giờ tăng ca, số dư phép năm, quẹt thẻ hôm nay)
    /// </summary>
    [HttpGet("employee")]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDashboardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmployeeDashboardDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetEmployeeDashboard()
    {
        var (_, currentEmployeeId, _) = GetCurrentUserContext();
        var result = await _dashboardService.GetEmployeeDashboardAsync(currentEmployeeId);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P3.2: Dashboard Quản lý phòng ban (Quân số đi làm/vắng mặt hôm nay, đơn nghỉ và giải trình cần duyệt)
    /// </summary>
    [HttpGet("manager")]
    [Authorize(Roles = "1,2,4,6,8,ADMIN,HR,MANAGER,Administration")]
    [ProducesResponseType(typeof(ApiResponse<ManagerDashboardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ManagerDashboardDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetManagerDashboard([FromQuery] string? departmentId = null)
    {
        var (_, currentEmployeeId, roles) = GetCurrentUserContext();
        var result = await _dashboardService.GetManagerDashboardAsync(departmentId, currentEmployeeId, roles);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P3.3: Dashboard Ban Giám đốc và Phòng Nhân sự (Toàn cảnh quân số công ty, tỷ lệ chuyên cần, biến động nhân sự)
    /// </summary>
    [HttpGet("hr")]
    [Authorize(Roles = "1,4,ADMIN,HR,Administration")]
    [ProducesResponseType(typeof(ApiResponse<HrDashboardDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<HrDashboardDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetHrDashboard()
    {
        var (_, _, roles) = GetCurrentUserContext();
        var result = await _dashboardService.GetHrDashboardAsync(roles);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}

