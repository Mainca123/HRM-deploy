using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/user-groups")]
[Produces("application/json")]
[Tags("Tài khoản & Nhóm quyền")]
public class UserGroupController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public UserGroupController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// Lấy danh sách các nhóm quyền trong hệ thống
    /// </summary>
    /// <response code="200">Lấy danh mục nhóm quyền thành công</response>
    [HttpGet]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    [ProducesResponseType(typeof(ApiResponse<List<UserGroupDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserGroups()
    {
        var result = await _organizationService.GetUserGroupsAsync();
        return Ok(result);
    }
}

