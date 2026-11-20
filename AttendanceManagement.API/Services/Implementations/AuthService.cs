using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AttendanceManagement.API.DTOs.Auth;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AttendanceManagement.API.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _config = config;
    }

    public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        // 1. Tìm thông tin User thông qua Repository
        var user = await _userRepository.GetByNameAsync(request.Username, includeGroup: true);
        if (user == null)
        {
            return ApiResponse<LoginResponseDto>.Fail("Tên đăng nhập hoặc mật khẩu không chính xác.");
        }

        // 2. Kiểm tra trạng thái kích hoạt tài khoản
        if (user.Permission == false)
        {
            return ApiResponse<LoginResponseDto>.Fail("Tài khoản của bạn đã bị khóa hoặc chưa được cấp quyền truy cập.");
        }

        // 3. Đối chiếu mật khẩu (Hỗ trợ BCrypt và chuỗi gốc cũ của DB)
        bool isPasswordValid = false;
        try
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            }
        }
        catch
        {
            // Fallback khi dữ liệu mẫu trong DB của thầy chưa được mã hóa
            isPasswordValid = (user.Password == request.Password);
        }

        if (!isPasswordValid)
        {
            return ApiResponse<LoginResponseDto>.Fail("Tên đăng nhập hoặc mật khẩu không chính xác.");
        }

        // 4. Lấy thông tin nhân viên liên kết thông qua Repository
        var userEmployee = await _userRepository.GetUserEmployeeAsync(user.Name);
        Employee? employee = null;
        if (userEmployee?.EmployeeId != null)
        {
            employee = await _userRepository.GetEmployeeByIdAsync(userEmployee.EmployeeId.Value);
        }

        // 5. Sinh mã JWT Bearer Token
        var (token, expiresAt) = GenerateJwtToken(user, employee?.Id, employee?.FullName);

        var response = new LoginResponseDto
        {
            Token = token,
            TokenType = "Bearer",
            ExpiresAt = expiresAt,
            User = new UserProfileDto
            {
                Username = user.Name,
                EmployeeId = employee?.Id,
                EmployeeCode = employee?.Code,
                FullName = employee?.FullName ?? user.Name,
                Role = user.GroupId ?? user.TypeId ?? "EMPLOYEE"
            }
        };

        return ApiResponse<LoginResponseDto>.Ok(response, "Đăng nhập thành công!");
    }

    public async Task<ApiResponse<UserProfileDto>> GetCurrentUserAsync(string username)
    {
        // 1. Tìm User
        var user = await _userRepository.GetByNameAsync(username);
        if (user == null)
        {
            return ApiResponse<UserProfileDto>.Fail("Không tìm thấy thông tin người dùng trong hệ thống.");
        }

        // 2. Tìm thông tin nhân viên liên kết
        var userEmployee = await _userRepository.GetUserEmployeeAsync(username);
        Employee? employee = null;
        if (userEmployee?.EmployeeId != null)
        {
            employee = await _userRepository.GetEmployeeByIdAsync(userEmployee.EmployeeId.Value);
        }

        var profile = new UserProfileDto
        {
            Username = user.Name,
            EmployeeId = employee?.Id,
            EmployeeCode = employee?.Code,
            FullName = employee?.FullName ?? user.Name,
            Role = user.GroupId ?? user.TypeId ?? "EMPLOYEE"
        };

        return ApiResponse<UserProfileDto>.Ok(profile, "Lấy thông tin tài khoản thành công.");
    }

    public async Task<ApiResponse<UserProfileDto>> CreateUserAsync(CreateUserRequestDto request)
    {
        // 1. Kiểm tra username đã được dùng chưa
        bool userExists = await _userRepository.ExistsByUsernameAsync(request.Username);
        if (userExists)
        {
            return ApiResponse<UserProfileDto>.Fail($"Tên đăng nhập '{request.Username}' đã tồn tại trong hệ thống.");
        }

        // 2. Kiểm tra nhân viên có tồn tại trong bảng Employee không
        var employee = await _userRepository.GetEmployeeByIdAsync(request.EmployeeId);
        if (employee == null)
        {
            return ApiResponse<UserProfileDto>.Fail($"Không tìm thấy hồ sơ nhân viên có ID = {request.EmployeeId}.");
        }

        // 3. Kiểm tra nhân viên này đã có tài khoản chưa
        bool alreadyHasUser = await _userRepository.EmployeeHasAccountAsync(request.EmployeeId);
        if (alreadyHasUser)
        {
            return ApiResponse<UserProfileDto>.Fail($"Nhân viên '{employee.FullName}' (Mã: {employee.Code}) đã được cấp tài khoản trước đó.");
        }

        // 4. Mã hóa mật khẩu bằng BCrypt
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // 5. Lưu đồng thời vào User và UserEmployee qua Transaction ở Repository
        try
        {
            var newUser = new User
            {
                Name = request.Username,
                Password = hashedPassword,
                GroupId = request.GroupId,
                TypeId = null,
                AuditData = 0,
                Permission = true
            };

            var newUserEmployee = new UserEmployee
            {
                UserName = request.Username,
                EmployeeId = request.EmployeeId
            };

            await _userRepository.CreateUserWithEmployeeAsync(newUser, newUserEmployee);

            var result = new UserProfileDto
            {
                Username = newUser.Name,
                EmployeeId = employee.Id,
                EmployeeCode = employee.Code,
                FullName = employee.FullName,
                Role = newUser.GroupId
            };

            return ApiResponse<UserProfileDto>.Ok(result, "Cấp tài khoản nhân viên thành công!");
        }
        catch (Exception ex)
        {
            return ApiResponse<UserProfileDto>.Fail($"Có lỗi xảy ra khi tạo tài khoản: {ex.Message}");
        }
    }

    public async Task<ApiResponse<bool>> ChangePasswordAsync(string username, ChangePasswordDto request)
    {
        var user = await _userRepository.GetByNameAsync(username);
        if (user == null)
        {
            return ApiResponse<bool>.Fail("Không tìm thấy tài khoản người dùng.");
        }

        // Kiểm tra mật khẩu cũ
        bool isOldPasswordValid = false;
        try
        {
            if (!string.IsNullOrEmpty(user.Password))
            {
                isOldPasswordValid = BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.Password);
            }
        }
        catch
        {
            isOldPasswordValid = (user.Password == request.CurrentPassword);
        }

        if (!isOldPasswordValid)
        {
            return ApiResponse<bool>.Fail("Mật khẩu hiện tại không chính xác.");
        }

        // Hash mật khẩu mới và cập nhật
        string newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _userRepository.UpdatePasswordAsync(user, newHashedPassword);

        return ApiResponse<bool>.Ok(true, "Đổi mật khẩu thành công!");
    }

    private (string Token, DateTime ExpiresAt) GenerateJwtToken(User user, int? employeeId, string? fullName)
    {
        var jwtSettings = _config.GetSection("JwtSettings");
        var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);
        var expiryDays = int.Parse(jwtSettings["ExpiryDays"] ?? "7");
        var expiresAt = DateTime.UtcNow.AddDays(expiryDays);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Name),
            new(ClaimTypes.Name, fullName ?? user.Name),
            new(ClaimTypes.Role, user.GroupId ?? user.TypeId ?? "EMPLOYEE"),
            new("GroupId", user.GroupId ?? ""),
            new("Username", user.Name)
        };

        if (employeeId.HasValue)
        {
            claims.Add(new Claim("EmployeeId", employeeId.Value.ToString()));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresAt,
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(secretKey),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return (tokenHandler.WriteToken(token), expiresAt);
    }
}
