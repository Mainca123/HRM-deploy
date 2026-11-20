using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Overtime;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class OvertimeService : IOvertimeService
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public OvertimeService(IOvertimeRepository overtimeRepository, IEmployeeRepository employeeRepository)
    {
        _overtimeRepository = overtimeRepository;
        _employeeRepository = employeeRepository;
    }

    private static bool IsAdmin(IList<string> roles) =>
        roles.Any(r => r.Equals("1", StringComparison.OrdinalIgnoreCase)
                    || r.Equals("ADMIN", StringComparison.OrdinalIgnoreCase)
                    || r.Equals("Administration", StringComparison.OrdinalIgnoreCase));

    public async Task<ApiResponse<OvertimeTypesResponseDto>> GetOvertimeTypesAsync()
    {
        var limits = await _overtimeRepository.GetOvertimeLimitsAsync();

        var types = new List<OvertimeTypeDto>
        {
            new() { Type = 1, Name = "Tăng ca ngày thường", Description = "Làm thêm giờ vào ngày làm việc bình thường" },
            new() { Type = 2, Name = "Tăng ca ngày nghỉ cuối tuần", Description = "Làm thêm giờ vào Thứ 7 / Chủ nhật" },
            new() { Type = 3, Name = "Tăng ca theo ca / chuẩn hệ thống", Description = "Loại mặc định đang sử dụng trong bảng RegisterOvertime (Value > 0: số giờ tối đa, 0: theo máy chấm công, -1: không tăng ca)" },
            new() { Type = 4, Name = "Tăng ca ngày Lễ / Tết", Description = "Làm thêm giờ vào các ngày nghỉ lễ theo quy định" }
        };

        var limitDtos = limits.Select(l => new OvertimeLimitDto
        {
            ShiftId = l.ShiftId,
            ReplacementShiftId = l.ReplacementShiftId,
            FromDate = l.FromDate,
            ToDate = l.ToDate,
            FromTime = l.FromTime,
            ToTime = l.ToTime,
            Value = l.Value,
            EmployeeIds = l.EmployeeIds
        }).ToList();

        var response = new OvertimeTypesResponseDto
        {
            Types = types,
            Limits = limitDtos
        };

        return ApiResponse<OvertimeTypesResponseDto>.Ok(response, "Lấy danh mục loại tăng ca và giới hạn thành công.");
    }

    public async Task<ApiResponse<PagedResultDto<OvertimeRequestDto>>> GetOvertimeRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? type,
        DateOnly? fromDate,
        DateOnly? toDate,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        if (!IsAdmin(roles))
        {
            if (!currentEmployeeId.HasValue)
            {
                return ApiResponse<PagedResultDto<OvertimeRequestDto>>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
            }
            employeeId = currentEmployeeId.Value;
            departmentId = null;
        }

        var result = await _overtimeRepository.GetOvertimeRequestsPagedAsync(
            pageIndex, pageSize, employeeId, departmentId, type, fromDate, toDate);

        return ApiResponse<PagedResultDto<OvertimeRequestDto>>.Ok(result, "Lấy danh sách đăng ký làm thêm giờ thành công.");
    }

    public async Task<ApiResponse<OvertimeRequestDto>> GetOvertimeRequestDetailAsync(
        int employeeId,
        DateOnly fromDate,
        byte type,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Bạn không có quyền xem đăng ký tăng ca của nhân viên khác.");
        }

        var detail = await _overtimeRepository.GetOvertimeRequestDetailAsync(employeeId, fromDate, type);
        if (detail == null)
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Không tìm thấy bản ghi đăng ký làm thêm giờ.");
        }

        return ApiResponse<OvertimeRequestDto>.Ok(detail, "Lấy chi tiết đăng ký làm thêm giờ thành công.");
    }

    public async Task<ApiResponse<OvertimeRequestDto>> CreateOvertimeRequestAsync(
        CreateOvertimeRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        int targetEmployeeId;
        if (IsAdmin(roles))
        {
            if (!request.EmployeeId.HasValue && !currentEmployeeId.HasValue)
            {
                return ApiResponse<OvertimeRequestDto>.Fail("Vui lòng truyền mã nhân viên (EmployeeId) cần đăng ký tăng ca.");
            }
            targetEmployeeId = request.EmployeeId ?? currentEmployeeId!.Value;
        }
        else
        {
            if (!currentEmployeeId.HasValue)
            {
                return ApiResponse<OvertimeRequestDto>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
            }
            if (request.EmployeeId.HasValue && request.EmployeeId.Value != currentEmployeeId.Value)
            {
                return ApiResponse<OvertimeRequestDto>.Fail("Bạn không có quyền đăng ký tăng ca hộ nhân viên khác.");
            }
            targetEmployeeId = currentEmployeeId.Value;
        }

        if (request.ToDate.HasValue && request.ToDate.Value < request.FromDate)
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Ngày kết thúc (ToDate) phải lớn hơn hoặc bằng ngày bắt đầu (FromDate).");
        }

        var employee = await _employeeRepository.GetByIdAsync(targetEmployeeId);
        if (employee == null)
        {
            return ApiResponse<OvertimeRequestDto>.Fail($"Không tìm thấy nhân viên với ID = {targetEmployeeId}.");
        }

        if (await _overtimeRepository.ExistsOvertimeRequestAsync(targetEmployeeId, request.FromDate, request.Type))
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Đã tồn tại bản ghi đăng ký tăng ca cho nhân viên này vào cùng ngày bắt đầu và loại tăng ca.");
        }

        var entity = new RegisterOvertime
        {
            EmployeeId = targetEmployeeId,
            FromDate = request.FromDate,
            ToDate = request.ToDate ?? request.FromDate,
            Type = request.Type,
            Value = request.Value,
            CreatedUserName = currentUsername,
            CreatedDate = DateTime.Now
        };

        await _overtimeRepository.CreateOvertimeRequestAsync(entity);

        var detail = await _overtimeRepository.GetOvertimeRequestDetailAsync(targetEmployeeId, request.FromDate, request.Type);
        return ApiResponse<OvertimeRequestDto>.Ok(detail!, "Đăng ký làm thêm giờ thành công.");
    }

    public async Task<ApiResponse<OvertimeRequestDto>> UpdateOvertimeRequestAsync(
        UpdateOvertimeRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != request.EmployeeId))
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Bạn không có quyền chỉnh sửa đăng ký tăng ca của nhân viên khác.");
        }

        if (request.ToDate.HasValue && request.ToDate.Value < request.FromDate)
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Ngày kết thúc (ToDate) phải lớn hơn hoặc bằng ngày bắt đầu (FromDate).");
        }

        var entity = await _overtimeRepository.GetOvertimeRequestEntityAsync(request.EmployeeId, request.FromDate, request.Type);
        if (entity == null)
        {
            return ApiResponse<OvertimeRequestDto>.Fail("Không tìm thấy bản ghi đăng ký làm thêm giờ cần cập nhật.");
        }

        entity.ToDate = request.ToDate ?? entity.ToDate;
        entity.Value = request.Value;
        entity.ModifiedUserName = currentUsername;
        entity.ModifiedDate = DateTime.Now;

        await _overtimeRepository.UpdateOvertimeRequestAsync(entity);

        var detail = await _overtimeRepository.GetOvertimeRequestDetailAsync(request.EmployeeId, request.FromDate, request.Type);
        return ApiResponse<OvertimeRequestDto>.Ok(detail!, "Cập nhật / xác nhận đăng ký làm thêm giờ thành công.");
    }

    public async Task<ApiResponse<bool>> DeleteOvertimeRequestAsync(
        int employeeId,
        DateOnly fromDate,
        byte type,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<bool>.Fail("Bạn không có quyền xóa đăng ký tăng ca của nhân viên khác.");
        }

        var entity = await _overtimeRepository.GetOvertimeRequestEntityAsync(employeeId, fromDate, type);
        if (entity == null)
        {
            return ApiResponse<bool>.Fail("Không tìm thấy bản ghi đăng ký làm thêm giờ cần xóa.");
        }

        await _overtimeRepository.DeleteOvertimeRequestAsync(entity);
        return ApiResponse<bool>.Ok(true, "Xóa đăng ký làm thêm giờ thành công.");
    }
}

