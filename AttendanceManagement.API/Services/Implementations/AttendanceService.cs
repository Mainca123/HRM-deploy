using AttendanceManagement.API.DTOs.Attendance;
using AttendanceManagement.API.DTOs.Common;
using AttendanceManagement.API.Entities;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Interfaces;

namespace AttendanceManagement.API.Services.Implementations;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AttendanceService(IAttendanceRepository attendanceRepository, IEmployeeRepository employeeRepository)
    {
        _attendanceRepository = attendanceRepository;
        _employeeRepository = employeeRepository;
    }

    private static bool IsAdminOrManager(IList<string> roles) =>
        roles.Any(r => r is "1" or "2" or "4" or "6" or "8" or "ADMIN" or "HR" or "MANAGER" or "Administration");

    public async Task<ApiResponse<List<ShiftDto>>> GetShiftsAsync()
    {
        var shifts = await _attendanceRepository.GetShiftsAsync();
        return ApiResponse<List<ShiftDto>>.Ok(shifts, "Lấy danh mục ca làm việc thành công.");
    }

    public async Task<ApiResponse<PagedResultDto<TimeInTimeOutDto>>> GetDailyAttendancePagedAsync(
        int pageIndex,
        int pageSize,
        DateTime? date,
        string? departmentId,
        string? search,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        if (!IsAdminOrManager(roles))
        {
            return ApiResponse<PagedResultDto<TimeInTimeOutDto>>.Fail("Bạn không có quyền xem dữ liệu chấm công phòng ban.");
        }

        var targetDate = date ?? DateTime.Today;
        var result = await _attendanceRepository.GetDailyAttendancePagedAsync(pageIndex, pageSize, targetDate, departmentId, search);

        return ApiResponse<PagedResultDto<TimeInTimeOutDto>>.Ok(result, $"Lấy dữ liệu chấm công ngày {targetDate:yyyy-MM-dd} thành công.");
    }

    public async Task<ApiResponse<List<TimeInTimeOutDto>>> GetMyAttendanceHistoryAsync(
        int? currentEmployeeId,
        int? month,
        int? year)
    {
        if (!currentEmployeeId.HasValue)
        {
            return ApiResponse<List<TimeInTimeOutDto>>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
        }

        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        var history = await _attendanceRepository.GetMyAttendanceHistoryAsync(currentEmployeeId.Value, targetMonth, targetYear);
        return ApiResponse<List<TimeInTimeOutDto>>.Ok(history, $"Lấy lịch sử chấm công tháng {targetMonth}/{targetYear} thành công.");
    }

    public async Task<ApiResponse<TimeInTimeOutDto>> CheckInAsync(
        CheckInRequestDto request,
        int? currentEmployeeId,
        string currentUsername)
    {
        if (!currentEmployeeId.HasValue)
        {
            return ApiResponse<TimeInTimeOutDto>.Fail("Tài khoản của bạn chưa được liên kết với hồ sơ nhân viên.");
        }

        var employeeId = currentEmployeeId.Value;
        var today = DateTime.Today;
        var now = DateTime.Now;

        // Xác định ca làm việc: nếu không truyền thì lấy ca mặc định IsDefault = true (chính là ca "HC" trong DB của thầy)
        string shiftId;
        if (!string.IsNullOrWhiteSpace(request.ShiftId))
        {
            var shift = await _attendanceRepository.GetShiftByIdAsync(request.ShiftId.Trim());
            if (shift == null)
            {
                return ApiResponse<TimeInTimeOutDto>.Fail($"Ca làm việc '{request.ShiftId}' không tồn tại trong hệ thống.");
            }
            shiftId = shift.Id;
        }
        else
        {
            var defaultShift = await _attendanceRepository.GetDefaultShiftAsync();
            if (defaultShift == null)
            {
                return ApiResponse<TimeInTimeOutDto>.Fail("Hệ thống chưa cấu hình ca làm việc mặc định.");
            }
            shiftId = defaultShift.Id;
        }

        // Kiểm tra xem đã có bản ghi Check-in trong ngày hôm nay cho ca này chưa
        var existingRecord = await _attendanceRepository.GetTodayTimeRecordAsync(employeeId, today, shiftId);
        if (existingRecord != null && existingRecord.TimeIn.HasValue)
        {
            return ApiResponse<TimeInTimeOutDto>.Fail($"Bạn đã thực hiện Check-in ca '{shiftId}' ngày hôm nay lúc {existingRecord.TimeIn.Value:HH:mm:ss}.");
        }

        if (existingRecord == null)
        {
            existingRecord = new TimeInTimeOut
            {
                EmployeeId = employeeId,
                WorkingDay = today,
                ShiftId = shiftId,
                TimeIn = now,
                Status = 0,
                Notes = string.IsNullOrWhiteSpace(request.Notes) ? $"Check-in Web lúc {now:HH:mm:ss}" : request.Notes.Trim()
            };
            await _attendanceRepository.CreateTimeInTimeOutAsync(existingRecord);
        }
        else
        {
            existingRecord.TimeIn = now;
            if (!string.IsNullOrWhiteSpace(request.Notes))
            {
                existingRecord.Notes = $"{existingRecord.Notes} | {request.Notes.Trim()}";
            }
            await _attendanceRepository.UpdateTimeInTimeOutAsync(existingRecord);
        }

        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        var shiftEntity = await _attendanceRepository.GetShiftByIdAsync(shiftId);

        var dto = new TimeInTimeOutDto
        {
            EmployeeId = employeeId,
            EmployeeCode = employee?.Code,
            EmployeeFullName = employee?.FullName ?? currentUsername,
            WorkingDay = today,
            ShiftId = shiftId,
            ShiftName = shiftEntity?.Name,
            TimeIn = now,
            Status = existingRecord.Status,
            Notes = existingRecord.Notes
        };

        return ApiResponse<TimeInTimeOutDto>.Ok(dto, $"Check-in ca '{shiftEntity?.Name ?? shiftId}' thành công lúc {now:HH:mm:ss}.");
    }

    public async Task<ApiResponse<TimeInTimeOutDto>> CheckOutAsync(
        CheckOutRequestDto request,
        int? currentEmployeeId,
        string currentUsername)
    {
        if (!currentEmployeeId.HasValue)
        {
            return ApiResponse<TimeInTimeOutDto>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
        }

        var employeeId = currentEmployeeId.Value;
        var today = DateTime.Today;
        var now = DateTime.Now;

        // Tìm bản ghi quẹt thẻ ngày hôm nay
        var record = await _attendanceRepository.GetTodayTimeRecordAsync(employeeId, today);
        if (record == null || !record.TimeIn.HasValue)
        {
            return ApiResponse<TimeInTimeOutDto>.Fail("Không tìm thấy dữ liệu Check-in ngày hôm nay. Vui lòng Check-in trước khi Check-out.");
        }

        record.TimeOut = now;
        if (!string.IsNullOrWhiteSpace(request.Notes))
        {
            record.Notes = string.IsNullOrEmpty(record.Notes)
                ? $"Check-out: {request.Notes.Trim()}"
                : $"{record.Notes} | Check-out: {request.Notes.Trim()}";
        }

        await _attendanceRepository.UpdateTimeInTimeOutAsync(record);

        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        var shift = await _attendanceRepository.GetShiftByIdAsync(record.ShiftId);

        var workingHours = record.TimeIn.HasValue
            ? Math.Round((now - record.TimeIn.Value).TotalHours, 1)
            : 0;

        var dto = new TimeInTimeOutDto
        {
            EmployeeId = employeeId,
            EmployeeCode = employee?.Code,
            EmployeeFullName = employee?.FullName ?? currentUsername,
            WorkingDay = today,
            ShiftId = record.ShiftId,
            ShiftName = shift?.Name,
            TimeIn = record.TimeIn,
            TimeOut = now,
            Status = record.Status,
            Notes = record.Notes,
            WorkingHours = workingHours
        };

        return ApiResponse<TimeInTimeOutDto>.Ok(dto, $"Check-out thành công lúc {now:HH:mm:ss}. Tổng thời gian làm việc: {workingHours} giờ.");
    }

    public async Task<ApiResponse<PagedResultDto<RegisterLateEarlyDto>>> GetLateEarlyRequestsPagedAsync(
        int pageIndex,
        int pageSize,
        int? employeeId,
        DateTime? fromDate,
        DateTime? toDate,
        int? currentEmployeeId,
        IList<string> roles)
    {
        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        if (!IsAdminOrManager(roles))
        {
            if (!currentEmployeeId.HasValue)
            {
                return ApiResponse<PagedResultDto<RegisterLateEarlyDto>>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
            }
            employeeId = currentEmployeeId.Value;
        }

        var result = await _attendanceRepository.GetLateEarlyRequestsPagedAsync(pageIndex, pageSize, employeeId, fromDate, toDate);
        return ApiResponse<PagedResultDto<RegisterLateEarlyDto>>.Ok(result, "Lấy danh sách đơn giải trình đi muộn / về sớm thành công.");
    }

    public async Task<ApiResponse<RegisterLateEarlyDto>> CreateLateEarlyRequestAsync(
        CreateLateEarlyRequestDto request,
        int? currentEmployeeId,
        string currentUsername)
    {
        if (!currentEmployeeId.HasValue)
        {
            return ApiResponse<RegisterLateEarlyDto>.Fail("Tài khoản chưa được liên kết với hồ sơ nhân viên.");
        }

        var employeeId = currentEmployeeId.Value;
        var fromDate = request.FromDate.Date;

        var existing = await _attendanceRepository.GetLateEarlyEntityAsync(employeeId, fromDate, request.Type);
        if (existing != null)
        {
            return ApiResponse<RegisterLateEarlyDto>.Fail($"Đã tồn tại đơn giải trình {(request.Type ? "về sớm" : "đi muộn")} cho ngày này.");
        }

        var entity = new RegisterLateEarly
        {
            EmployeeId = employeeId,
            FromDate = fromDate,
            ToDate = fromDate,
            Type = request.Type,
            Value = request.Value,
            Reason = request.Reason.Trim(),
            CreatedUserName = currentUsername,
            CreatedDate = DateTime.Now
        };

        await _attendanceRepository.CreateLateEarlyAsync(entity);

        var employee = await _employeeRepository.GetByIdAsync(employeeId);

        var dto = new RegisterLateEarlyDto
        {
            EmployeeId = employeeId,
            EmployeeCode = employee?.Code,
            EmployeeFullName = employee?.FullName ?? currentUsername,
            FromDate = fromDate,
            ToDate = fromDate,
            Type = request.Type,
            Value = request.Value,
            Reason = request.Reason.Trim(),
            CreatedUserName = currentUsername,
            CreatedDate = entity.CreatedDate
        };

        return ApiResponse<RegisterLateEarlyDto>.Ok(dto, "Gửi đơn giải trình đi muộn / về sớm thành công.");
    }

    public async Task<ApiResponse<RegisterLateEarlyDto>> ApproveLateEarlyRequestAsync(
        ApproveLateEarlyRequestDto request,
        string currentUsername,
        IList<string> roles)
    {
        if (!roles.Any(r => r is "1" or "ADMIN" or "Administration"))
        {
            return ApiResponse<RegisterLateEarlyDto>.Fail("Chỉ Quản trị viên (Admin - Nhóm 1) mới có quyền phê duyệt đơn giải trình.");
        }

        var entity = await _attendanceRepository.GetLateEarlyEntityAsync(request.EmployeeId, request.FromDate.Date, request.Type);
        if (entity == null)
        {
            return ApiResponse<RegisterLateEarlyDto>.Fail("Không tìm thấy đơn giải trình cần phê duyệt.");
        }

        if (!string.IsNullOrEmpty(entity.ModifiedUserName))
        {
            return ApiResponse<RegisterLateEarlyDto>.Fail("Đơn giải trình này đã được phê duyệt trước đó.");
        }

        entity.ModifiedUserName = currentUsername;
        entity.ModifiedDate = DateTime.Now;
        if (!string.IsNullOrWhiteSpace(request.AdminNote))
        {
            entity.Reason = $"{entity.Reason} | [Admin duyệt: {request.AdminNote.Trim()}]";
        }

        await _attendanceRepository.UpdateLateEarlyAsync(entity);

        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

        var dto = new RegisterLateEarlyDto
        {
            EmployeeId = entity.EmployeeId,
            EmployeeCode = employee?.Code,
            EmployeeFullName = employee?.FullName ?? string.Empty,
            FromDate = entity.FromDate,
            ToDate = entity.ToDate,
            Type = entity.Type,
            Value = entity.Value,
            Reason = entity.Reason,
            CreatedUserName = entity.CreatedUserName,
            CreatedDate = entity.CreatedDate,
            ModifiedUserName = entity.ModifiedUserName,
            ModifiedDate = entity.ModifiedDate
        };

        return ApiResponse<RegisterLateEarlyDto>.Ok(dto, "Phê duyệt đơn giải trình đi muộn / về sớm thành công.");
    }

    public async Task<ApiResponse<bool>> DeleteLateEarlyRequestAsync(
        int employeeId,
        DateTime fromDate,
        bool type,
        int? currentEmployeeId,
        IList<string> roles)
    {
        bool isAdmin = roles.Any(r => r is "1" or "ADMIN" or "Administration");

        if (!isAdmin && (!currentEmployeeId.HasValue || currentEmployeeId.Value != employeeId))
        {
            return ApiResponse<bool>.Fail("Bạn không có quyền xóa đơn giải trình của nhân viên khác.");
        }

        var entity = await _attendanceRepository.GetLateEarlyEntityAsync(employeeId, fromDate.Date, type);
        if (entity == null)
        {
            return ApiResponse<bool>.Fail("Không tìm thấy đơn giải trình cần xóa.");
        }

        if (!isAdmin && !string.IsNullOrEmpty(entity.ModifiedUserName))
        {
            return ApiResponse<bool>.Fail("Đơn giải trình này đã được phê duyệt, bạn không thể tự ý xóa.");
        }

        await _attendanceRepository.DeleteLateEarlyAsync(entity);
        return ApiResponse<bool>.Ok(true, "Xóa đơn giải trình thành công.");
    }

    public async Task<ApiResponse<PagedResultDto<MonthlyAttendanceSummaryDto>>> GetMonthlyAttendanceSummaryPagedAsync(
        int pageIndex,
        int pageSize,
        int? month,
        int? year,
        string? departmentId,
        string? search,
        IList<string> roles)
    {
        if (!IsAdminOrManager(roles))
        {
            return ApiResponse<PagedResultDto<MonthlyAttendanceSummaryDto>>.Fail("Bạn không có quyền xem bảng tổng hợp công tháng.");
        }

        if (pageIndex < 1) pageIndex = 1;
        if (pageSize < 1) pageSize = 10;
        if (pageSize > 100) pageSize = 100;

        var targetMonth = month ?? DateTime.Today.Month;
        var targetYear = year ?? DateTime.Today.Year;

        var result = await _attendanceRepository.GetMonthlyAttendanceSummaryPagedAsync(
            pageIndex, pageSize, targetMonth, targetYear, departmentId, search);

        return ApiResponse<PagedResultDto<MonthlyAttendanceSummaryDto>>.Ok(result, $"Lấy bảng tổng hợp công tháng {targetMonth}/{targetYear} thành công.");
    }
}

