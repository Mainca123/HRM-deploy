using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Employee;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/departments")]
[Produces("application/json")]
[Tags("Tổ chức & Danh mục")]
public class DepartmentController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public DepartmentController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Lấy danh sách toàn bộ phòng ban trong công ty
    /// </summary>
    /// <param name="activeOnly">Nếu true thì chỉ lấy phòng ban đang hoạt động</param>
    /// <response code="200">Lấy danh mục phòng ban thành công</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ApiResponse<List<DepartmentDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartments([FromQuery] bool activeOnly = false)
    {
        var result = await _organizationService.GetDepartmentsAsync(activeOnly);
        return Ok(result);
    }
}

