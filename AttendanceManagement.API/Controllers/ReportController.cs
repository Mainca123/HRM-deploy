using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Dashboard;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/reports")]
[Produces("application/json")]
[Tags("Báo cáo Tổng hợp Nghiệp vụ (P3)")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
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
    /// P3.4: Báo cáo tổng hợp số ngày nghỉ phép theo loại (Phép năm, Ốm, Không lương, Thai sản...) và phòng ban
    /// </summary>
    [HttpGet("leave-summary")]
    [Authorize(Roles = "1,2,4,6,8,ADMIN,HR,MANAGER,Administration")]
    [ProducesResponseType(typeof(ApiResponse<List<LeaveSummaryReportDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<LeaveSummaryReportDto>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetLeaveSummaryReport(
        [FromQuery] DateTime? fromDate = null,
        [FromQuery] DateTime? toDate = null,
        [FromQuery] string? departmentId = null)
    {
        var (_, _, roles) = GetCurrentUserContext();
        var result = await _reportService.GetLeaveSummaryReportAsync(fromDate, toDate, departmentId, roles);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// P3.5: Báo cáo tổng hợp số giờ làm thêm giờ theo hệ số (Ngày thường OT1, Đêm OT2, Nghỉ OT3/4, Lễ OT5/6)
    /// </summary>
    [HttpGet("overtime-summary")]
    [Authorize(Roles = "1,2,4,6,8,ADMIN,HR,MANAGER,Administration")]
    [ProducesResponseType(typeof(ApiResponse<List<OvertimeSummaryReportDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<OvertimeSummaryReportDto>>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetOvertimeSummaryReport(
        [FromQuery] int? month = null,
        [FromQuery] int? year = null,
        [FromQuery] string? departmentId = null)
    {
        var (_, _, roles) = GetCurrentUserContext();
        var result = await _reportService.GetOvertimeSummaryReportAsync(month, year, departmentId, roles);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}

