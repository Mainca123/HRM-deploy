using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AttendanceManagement.API.Data;

namespace AttendanceManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseTestController : ControllerBase
{
    private readonly AppDbContext _context;

    // Nhận AppDbContext từ Dependency Injection
    public DatabaseTestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("connection")]
    public async Task<IActionResult> CheckConnection()
    {
        try
        {
            // Kiểm tra khả năng kết nối TiDB Cloud / MySQL
            bool connected = await _context.Database.CanConnectAsync();

            if (!connected)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Không thể kết nối cơ sở dữ liệu TiDB Cloud / MySQL."
                });
            }

            return Ok(new
            {
                success = true,
                message = "Kết nối TiDB Cloud / MySQL thành công!"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = "Kết nối database thất bại.",
                error = ex.Message
            });
        }
    }
}
