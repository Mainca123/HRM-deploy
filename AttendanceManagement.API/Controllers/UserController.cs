using System.Security.Claims;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
[Tags("Quản lý Người dùng")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Lấy danh sách tài khoản người dùng có phân trang, tìm kiếm và lọc theo nhóm quyền
    /// </summary>
    /// <param name="pageIndex">Trang hiện tại (mặc định: 1)</param>
    /// <param name="pageSize">Số bản ghi trên trang (mặc định: 10, tối đa: 100)</param>
    /// <param name="search">Tìm kiếm theo tên đăng nhập hoặc họ tên nhân viên liên kết</param>
    /// <param name="groupId">Mã nhóm quyền cần lọc</param>
    [HttpGet]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    [ProducesResponseType(typeof(ApiResponse<PagedResultDto<UserSummaryDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? search = null,
        [FromQuery] string? groupId = null)
    {
        var result = await _userService.GetUsersPagedAsync(pageIndex, pageSize, search, groupId);
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin chi tiết một tài khoản người dùng
    /// </summary>
    /// <param name="username">Tên đăng nhập cần tra cứu</param>
    [HttpGet("{username}")]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    [ProducesResponseType(typeof(ApiResponse<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var result = await _userService.GetUserDetailByNameAsync(username);
        if (!result.Success)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Cập nhật nhóm quyền, trạng thái kích hoạt hoặc nhân viên liên kết của tài khoản
    /// </summary>
    /// <param name="username">Tên đăng nhập cần cập nhật</param>
    /// <param name="request">Thông tin cập nhật</param>
    [HttpPut("{username}")]
    [Authorize(Roles = "1,ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserDetailDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<UserDetailDto>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(string username, [FromBody] UpdateUserRequestDto request)
    {
        var result = await _userService.UpdateUserAsync(username, request);
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
    /// Đặt lại mật khẩu tài khoản người dùng (Admin reset password)
    /// </summary>
    /// <param name="username">Tên đăng nhập cần đặt lại mật khẩu</param>
    /// <param name="request">Mật khẩu mới (mặc định nếu trống là 123456)</param>
    [HttpPost("{username}/reset-password")]
    [Authorize(Roles = "1,ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(string username, [FromBody] AdminResetPasswordDto request)
    {
        var result = await _userService.ResetPasswordAsync(username, request);
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
    /// Xóa tài khoản người dùng khỏi hệ thống
    /// </summary>
    /// <param name="username">Tên đăng nhập cần xóa</param>
    [HttpDelete("{username}")]
    [Authorize(Roles = "1,ADMIN")]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(string username)
    {
        var currentUsername = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Username") ?? string.Empty;

        var result = await _userService.DeleteUserAsync(username, currentUsername);
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

