using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.User;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IOrganizationRepository _organizationRepository;

    public UserService(
        IUserRepository userRepository,
        IEmployeeRepository employeeRepository,
        IOrganizationRepository organizationRepository)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _organizationRepository = organizationRepository;
    }

    public async Task<ApiResponse<PagedResultDto<UserSummaryDto>>> GetUsersPagedAsync(
        int pageIndex,
        int pageSize,
        string? search,
        string? groupId)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var result = await _userRepository.GetUsersPagedAsync(pageIndex, pageSize, search, groupId);
        return ApiResponse<PagedResultDto<UserSummaryDto>>.Ok(result, "Lấy danh sách tài khoản thành công.");
    }

    public async Task<ApiResponse<UserDetailDto>> GetUserDetailByNameAsync(string username)
    {
        var userDetail = await _userRepository.GetUserDetailByNameAsync(username);
        if (userDetail == null)
        {
            return ApiResponse<UserDetailDto>.Fail($"Không tìm thấy tài khoản với tên đăng nhập '{username}'.");
        }

        return ApiResponse<UserDetailDto>.Ok(userDetail, "Lấy thông tin tài khoản thành công.");
    }

    public async Task<ApiResponse<UserDetailDto>> UpdateUserAsync(string username, UpdateUserRequestDto request)
    {
        var user = await _userRepository.GetByNameAsync(username);
        if (user == null)
        {
            return ApiResponse<UserDetailDto>.Fail($"Không tìm thấy tài khoản '{username}' để cập nhật.");
        }

        // Không cho phép khóa tài khoản Administrator chính
        if (username.Equals("Administrator", StringComparison.OrdinalIgnoreCase) && !request.Permission)
        {
            return ApiResponse<UserDetailDto>.Fail("Không được phép khóa tài khoản Administrator hệ thống.");
        }

        // Kiểm tra nhóm quyền
        if (!await _organizationRepository.UserGroupExistsAsync(request.GroupId))
        {
            return ApiResponse<UserDetailDto>.Fail($"Nhóm quyền với mã '{request.GroupId}' không tồn tại.");
        }

        // Kiểm tra nhân viên liên kết nếu có
        if (request.EmployeeId.HasValue)
        {
            var emp = await _employeeRepository.GetByIdAsync(request.EmployeeId.Value);
            if (emp == null)
            {
                return ApiResponse<UserDetailDto>.Fail($"Nhân viên với ID '{request.EmployeeId.Value}' không tồn tại.");
            }

            var currentLink = await _userRepository.GetUserEmployeeAsync(username);
            if (currentLink == null || currentLink.EmployeeId != request.EmployeeId.Value)
            {
                if (await _userRepository.EmployeeHasAccountAsync(request.EmployeeId.Value))
                {
                    return ApiResponse<UserDetailDto>.Fail($"Nhân viên '{emp.FullName}' đã được liên kết với một tài khoản khác.");
                }
            }
        }

        user.GroupId = request.GroupId;
        user.Permission = request.Permission;

        await _userRepository.UpdateUserWithEmployeeAsync(user, request.EmployeeId);

        var updatedDetail = await _userRepository.GetUserDetailByNameAsync(username);
        return ApiResponse<UserDetailDto>.Ok(updatedDetail!, "Cập nhật thông tin tài khoản thành công.");
    }

    public async Task<ApiResponse<bool>> ResetPasswordAsync(string username, AdminResetPasswordDto request)
    {
        var user = await _userRepository.GetByNameAsync(username);
        if (user == null)
        {
            return ApiResponse<bool>.Fail($"Không tìm thấy tài khoản '{username}'.");
        }

        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        await _userRepository.UpdatePasswordAsync(user, newHashedPassword);

        return ApiResponse<bool>.Ok(true, $"Đặt lại mật khẩu cho tài khoản '{username}' thành công.");
    }

    public async Task<ApiResponse<bool>> DeleteUserAsync(string username, string currentUsername)
    {
        if (username.Equals("Administrator", StringComparison.OrdinalIgnoreCase))
        {
            return ApiResponse<bool>.Fail("Tuyệt đối không được phép xóa tài khoản Administrator hệ thống.");
        }

        if (username.Equals(currentUsername, StringComparison.OrdinalIgnoreCase))
        {
            return ApiResponse<bool>.Fail("Bạn không thể tự xóa chính tài khoản đang đăng nhập của mình.");
        }

        var user = await _userRepository.GetByNameAsync(username);
        if (user == null)
        {
            return ApiResponse<bool>.Fail($"Không tìm thấy tài khoản '{username}' cần xóa.");
        }

        await _userRepository.DeleteUserAsync(username);
        return ApiResponse<bool>.Ok(true, $"Xóa tài khoản '{username}' thành công.");
    }
}

