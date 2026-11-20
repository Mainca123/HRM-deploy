using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/positions")]
[Produces("application/json")]
[Tags("Tổ chức & Danh mục")]
public class PositionController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public PositionController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Lấy danh sách toàn bộ chức vụ trong công ty
    /// </summary>
    /// <response code="200">Lấy danh mục chức vụ thành công</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<PositionDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPositions()
    {
        var result = await _organizationService.GetPositionsAsync();
        return Ok(result);
    }
}

