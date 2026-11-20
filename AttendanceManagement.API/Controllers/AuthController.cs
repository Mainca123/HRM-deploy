using System.Security.Claims;
using AttendanceManagement.API.DTOs.Auth;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Xác thực & Phân quyền")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Đăng nhập hệ thống và cấp JWT Token
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);
        if (!result.Success)
        {
            return Unauthorized(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Lấy thông tin tài khoản và nhân viên đang đăng nhập
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Username");
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(ApiResponse<UserProfileDto>.Fail("Token không hợp lệ."));
        }

        var result = await _authService.GetCurrentUserAsync(username);
        return Ok(result);
    }

    /// <summary>
    /// Cấp tài khoản mới cho nhân viên (Chỉ Quản trị viên '1' hoặc Nhân sự '4')
    /// </summary>
    [HttpPost("users")]
    [Authorize(Roles = "1,4,ADMIN,HR")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
    {
        var result = await _authService.CreateUserAsync(request);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Đổi mật khẩu tài khoản hiện tại
    /// </summary>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("Username");
        if (string.IsNullOrEmpty(username))
        {
            return Unauthorized(ApiResponse<bool>.Fail("Token không hợp lệ."));
        }

        var result = await _authService.ChangePasswordAsync(username, request);
        if (!result.Success)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    /// <summary>
    /// Đăng xuất khỏi hệ thống
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        return Ok(ApiResponse<string>.Ok("Đăng xuất thành công!"));
    }
}
