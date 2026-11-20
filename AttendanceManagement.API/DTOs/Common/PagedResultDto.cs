namespace AttendanceManagement.API.DTOs.Common;

/// <summary>
/// Cấu trúc dữ liệu phân trang dùng chung cho các API danh sách
/// </summary>
/// <typeparam name="T">Kiểu dữ liệu của các phần tử trong danh sách</typeparam>
public class PagedResultDto<T>
{
    /// <summary>
    /// Danh sách các phần tử thuộc trang hiện tại
    /// </summary>
    public List<T> Items { get; set; } = new();

    /// <summary>
    /// Tổng số lượng bản ghi thỏa mãn điều kiện lọc
    /// </summary>
    /// <example>100</example>
    public int TotalCount { get; set; }

    /// <summary>
    /// Trang hiện tại (bắt đầu từ 1)
    /// </summary>
    /// <example>1</example>
    public int PageIndex { get; set; }

    /// <summary>
    /// Số lượng bản ghi trên một trang
    /// </summary>
    /// <example>10</example>
    public int PageSize { get; set; }

    /// <summary>
    /// Tổng số trang tính toán tự động
    /// </summary>
    /// <example>10</example>
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling((double)TotalCount / PageSize) : 0;
}

