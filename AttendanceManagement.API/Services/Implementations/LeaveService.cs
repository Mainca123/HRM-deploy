using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.DTOs.Leave;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _leaveRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository)
    {
        _leaveRepository = leaveRepository;
        _employeeRepository = employeeRepository;
    }

    private static bool IsAdmin(IList<string> roles) =>
        roles.Any(r => r.Equals("1", StringComparison.OrdinalIgnoreCase)
                    || r.Equals("ADMIN", StringComparison.OrdinalIgnoreCase)
                    || r.Equals("Administration", StringComparison.OrdinalIgnoreCase));

    public async Task<ApiResponse<List<AbsenceTypeDto>>> GetAbsenceTypesAsync()
    {
        var types = await _leaveRepository.GetAbsenceTypesAsync();
        var dtos = types.Select(a => new AbsenceTypeDto
        {
            Id = a.Id,
            Name = a.Name,
            Company = a.Company,
            Si = a.Si
        }).ToList();

        return ApiResponse<List<AbsenceTypeDto>>.Ok(dtos, "Lấy danh mục loại nghỉ phép thành công.");
    }

    public async Task<ApiResponse<PagedResultDto<LeaveRequestDto>>> GetLeaveRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        string? departmentId,
        byte? status,
        DateTime? fromDate,
        DateTime? toDate,
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
                return ApiResponse<PagedResultDto<LeaveRequestDto>>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
            }
            // Nhân viên thường chỉ được xem đơn của chính mình
            employeeId = currentEmployeeId.Value;
            departmentId = null;
        }

        var result = await _leaveRepository.GetLeaveRequestsPagedAsync(
            pageIndex, pageSize, employeeId, departmentId, status, fromDate, toDate);

        return ApiResponse<PagedResultDto<LeaveRequestDto>>.Ok(result, "Lấy danh sách đơn nghỉ phép thành công.");
    }

    public async Task<ApiResponse<LeaveRequestDto>> GetLeaveRequestDetailAsync(
        int employeeId,
        DateTime fromDate,
        string absenceId,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Bạn không có quyền xem đơn nghỉ phép của nhân viên khác.");
        }

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(employeeId, fromDate, absenceId);
        if (detail == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Không tìm thấy đơn xin nghỉ phép.");
        }

        return ApiResponse<LeaveRequestDto>.Ok(detail, "Lấy chi tiết đơn nghỉ phép thành công.");
    }

    public async Task<ApiResponse<LeaveRequestDto>> CreateLeaveRequestAsync(
        CreateLeaveRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        int targetEmployeeId;
        if (IsAdmin(roles))
        {
            if (!request.EmployeeId.HasValue && !currentEmployeeId.HasValue)
            {
                return ApiResponse<LeaveRequestDto>.Fail("Vui lòng truyền mã nhân viên (EmployeeId) cần tạo đơn.");
            }
            targetEmployeeId = request.EmployeeId ?? currentEmployeeId!.Value;
        }
        else
        {
            if (!currentEmployeeId.HasValue)
            {
                return ApiResponse<LeaveRequestDto>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
            }
            if (request.EmployeeId.HasValue && request.EmployeeId.Value != currentEmployeeId.Value)
            {
                return ApiResponse<LeaveRequestDto>.Fail("Bạn không có quyền tạo đơn nghỉ phép cho nhân viên khác.");
            }
            targetEmployeeId = currentEmployeeId.Value;
        }

        if (request.ToDate < request.FromDate)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Ngày kết thúc nghỉ (ToDate) phải lớn hơn hoặc bằng ngày bắt đầu (FromDate).");
        }

        var employee = await _employeeRepository.GetByIdAsync(targetEmployeeId);
        if (employee == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail($"Không tìm thấy nhân viên với ID = {targetEmployeeId}.");
        }

        var absenceIdTrimmed = request.AbsenceId.Trim();
        if (!await _leaveRepository.AbsenceExistsAsync(absenceIdTrimmed))
        {
            return ApiResponse<LeaveRequestDto>.Fail($"Loại nghỉ phép '{absenceIdTrimmed}' không tồn tại trong hệ thống.");
        }

        var existing = await _leaveRepository.GetLeaveRequestEntityAsync(targetEmployeeId, request.FromDate, absenceIdTrimmed);
        if (existing != null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Đã tồn tại đơn nghỉ phép với cùng ngày bắt đầu và loại nghỉ phép này.");
        }

        if (await _leaveRepository.HasOverlappingLeaveAsync(targetEmployeeId, request.FromDate, request.ToDate))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Khoảng thời gian xin nghỉ bị trùng lặp với một đơn nghỉ phép khác đang chờ duyệt hoặc đã duyệt.");
        }

        var entity = new RegisterAbsence
        {
            EmployeeId = targetEmployeeId,
            FromDate = request.FromDate,
            ToDate = request.ToDate,
            Unit = request.Unit,
            AbsenceId = absenceIdTrimmed,
            Notes = request.Notes?.Trim(),
            Status = 0, // Chờ duyệt
            CreatedUserName = currentUsername,
            CreatedDate = DateTime.Now
        };

        await _leaveRepository.CreateLeaveRequestAsync(entity);

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(targetEmployeeId, request.FromDate, absenceIdTrimmed);
        return ApiResponse<LeaveRequestDto>.Ok(detail!, "Tạo đơn xin nghỉ phép thành công (đang chờ duyệt).");
    }

    public async Task<ApiResponse<LeaveRequestDto>> UpdateLeaveRequestAsync(
        UpdateLeaveRequestDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != request.EmployeeId))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Bạn không có quyền chỉnh sửa đơn nghỉ phép của nhân viên khác.");
        }

        if (request.ToDate < request.FromDate)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Ngày kết thúc nghỉ (ToDate) phải lớn hơn hoặc bằng ngày bắt đầu (FromDate).");
        }

        var absenceIdTrimmed = request.AbsenceId.Trim();
        var entity = await _leaveRepository.GetLeaveRequestEntityAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        if (entity == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Không tìm thấy đơn xin nghỉ phép cần chỉnh sửa.");
        }

        if (entity.Status != 0)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Chỉ được phép chỉnh sửa đơn khi đang ở trạng thái Chờ duyệt.");
        }

        if (await _leaveRepository.HasOverlappingLeaveAsync(
            request.EmployeeId, request.FromDate, request.ToDate, request.FromDate, absenceIdTrimmed))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Khoảng thời gian xin nghỉ mới bị trùng lặp với một đơn nghỉ phép khác.");
        }

        entity.ToDate = request.ToDate;
        entity.Unit = request.Unit;
        entity.Notes = request.Notes?.Trim();
        entity.ModifiedUserName = currentUsername;
        entity.ModifiedDate = DateTime.Now;

        await _leaveRepository.UpdateLeaveRequestAsync(entity);

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        return ApiResponse<LeaveRequestDto>.Ok(detail!, "Cập nhật đơn xin nghỉ phép thành công.");
    }

    public async Task<ApiResponse<LeaveRequestDto>> ApproveLeaveRequestAsync(
        LeaveRequestKeyDto request,
        string currentUsername,
        IList<string> roles)
    {
        if (!IsAdmin(roles))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Chỉ Quản trị viên (Admin - Nhóm 1) mới có quyền phê duyệt đơn nghỉ phép.");
        }

        var absenceIdTrimmed = request.AbsenceId.Trim();
        var entity = await _leaveRepository.GetLeaveRequestEntityAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        if (entity == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Không tìm thấy đơn xin nghỉ phép cần phê duyệt.");
        }

        if (entity.Status != 0)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Đơn nghỉ phép này đã được xử lý trước đó, không thể phê duyệt lại.");
        }

        entity.Status = 1; // Đã duyệt
        entity.ApprovedUserName = currentUsername;
        entity.ApprovedDate = DateTime.Now;

        await _leaveRepository.UpdateLeaveRequestAsync(entity);

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        return ApiResponse<LeaveRequestDto>.Ok(detail!, "Phê duyệt đơn xin nghỉ phép thành công.");
    }

    public async Task<ApiResponse<LeaveRequestDto>> RejectLeaveRequestAsync(
        RejectLeaveRequestDto request,
        string currentUsername,
        IList<string> roles)
    {
        if (!IsAdmin(roles))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Chỉ Quản trị viên (Admin - Nhóm 1) mới có quyền từ chối đơn nghỉ phép.");
        }

        var absenceIdTrimmed = request.AbsenceId.Trim();
        var entity = await _leaveRepository.GetLeaveRequestEntityAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        if (entity == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Không tìm thấy đơn xin nghỉ phép cần từ chối.");
        }

        if (entity.Status != 0)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Đơn nghỉ phép này đã được xử lý trước đó, không thể từ chối.");
        }

        entity.Status = 2; // Từ chối
        entity.ApprovedUserName = currentUsername;
        entity.ApprovedDate = DateTime.Now;
        var reasonText = $"[Lý do từ chối: {request.RejectReason.Trim()}]";
        entity.Notes = string.IsNullOrWhiteSpace(entity.Notes)
            ? reasonText
            : $"{entity.Notes} | {reasonText}";

        await _leaveRepository.UpdateLeaveRequestAsync(entity);

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        return ApiResponse<LeaveRequestDto>.Ok(detail!, "Đã từ chối đơn xin nghỉ phép.");
    }

    public async Task<ApiResponse<LeaveRequestDto>> CancelLeaveRequestAsync(
        LeaveRequestKeyDto request,
        string currentUsername,
        int? currentEmployeeId,
        IList<string> roles)
    {
        bool isAdmin = IsAdmin(roles);
        if (!isAdmin && (!currentEmployeeId.HasValue || currentEmployeeId.Value != request.EmployeeId))
        {
            return ApiResponse<LeaveRequestDto>.Fail("Bạn không có quyền hủy đơn nghỉ phép của nhân viên khác.");
        }

        var absenceIdTrimmed = request.AbsenceId.Trim();
        var entity = await _leaveRepository.GetLeaveRequestEntityAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        if (entity == null)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Không tìm thấy đơn xin nghỉ phép cần hủy.");
        }

        if (!isAdmin && entity.Status != 0)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Bạn chỉ có thể hủy đơn khi đơn đang ở trạng thái Chờ duyệt.");
        }

        if (entity.Status == 3)
        {
            return ApiResponse<LeaveRequestDto>.Fail("Đơn nghỉ phép này đã được hủy trước đó.");
        }

        entity.Status = 3; // Đã hủy
        entity.ModifiedUserName = currentUsername;
        entity.ModifiedDate = DateTime.Now;

        await _leaveRepository.UpdateLeaveRequestAsync(entity);

        var detail = await _leaveRepository.GetLeaveRequestDetailAsync(request.EmployeeId, request.FromDate, absenceIdTrimmed);
        return ApiResponse<LeaveRequestDto>.Ok(detail!, "Hủy đơn xin nghỉ phép thành công.");
    }

    public async Task<ApiResponse<bool>> DeleteLeaveRequestAsync(
        int employeeId,
        DateTime fromDate,
        string absenceId,
        int? currentEmployeeId,
        IList<string> roles)
    {
        bool isAdmin = IsAdmin(roles);
        if (!isAdmin && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<bool>.Fail("Bạn không có quyền xóa đơn nghỉ phép của nhân viên khác.");
        }

        var absenceIdTrimmed = absenceId.Trim();
        var entity = await _leaveRepository.GetLeaveRequestEntityAsync(employeeId, fromDate, absenceIdTrimmed);
        if (entity == null)
        {
            return ApiResponse<bool>.Fail("Không tìm thấy đơn xin nghỉ phép cần xóa.");
        }

        if (!isAdmin && entity.Status != 0)
        {
            return ApiResponse<bool>.Fail("Chỉ được phép xóa đơn khi đơn đang ở trạng thái Chờ duyệt (chưa được duyệt).");
        }

        await _leaveRepository.DeleteLeaveRequestAsync(entity);
        return ApiResponse<bool>.Ok(true, "Xóa đơn xin nghỉ phép thành công.");
    }

    public async Task<ApiResponse<LeaveBalanceDto>> GetAnnualLeaveBalanceAsync(
        int employeeId,
        short? year,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (!IsAdmin(roles) && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<LeaveBalanceDto>.Fail("Bạn không có quyền xem số dư phép của nhân viên khác.");
        }

        var targetYear = year ?? (short)DateTime.Today.Year;
        var balance = await _leaveRepository.GetAnnualLeaveBalanceAsync(employeeId, targetYear);
        if (balance == null)
        {
            return ApiResponse<LeaveBalanceDto>.Fail($"Không tìm thấy nhân viên với ID = {employeeId}.");
        }

        return ApiResponse<LeaveBalanceDto>.Ok(balance, $"Tra cứu số dư phép năm {targetYear} thành công.");
    }
}

