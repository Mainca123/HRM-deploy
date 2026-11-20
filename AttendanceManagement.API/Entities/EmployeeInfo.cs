using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class EmployeeInfo
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string FullName { get; set; } = null!;

    public string? Alias { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public bool? Sex { get; set; }

    public string Gender { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public string? NationalityName { get; set; }

    public string? EthnicGroupName { get; set; }

    public string? MaritalStatus { get; set; }

    public string? CultureLevel { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public string? DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public short? SortOrder { get; set; }

    public DateTime? InitialDate { get; set; }

    public DateTime? SigningDate { get; set; }

    public string? PositionId { get; set; }

    public string? PositionName { get; set; }

    public string? JobId { get; set; }

    public string? BirthPlace { get; set; }

    public string? CurrentAddress { get; set; }

    public string? Mobile { get; set; }

    public string? Home { get; set; }

    public string? Office { get; set; }

    public DateTime? CardFromDate { get; set; }

    public string? CardId { get; set; }

    public string? EnrollNumber { get; set; }

    public string? Idnumber { get; set; }

    public DateTime? IssuedDate { get; set; }

    public string? IssuedPlace { get; set; }

    public string? Notes { get; set; }

    public string? Photo { get; set; }

    public string? TaxCode { get; set; }

    public DateTime? DateOfIssueOfCode { get; set; }

    public string? FirstDependent { get; set; }

    public string? SecondDependent { get; set; }

    public string? BookNumber { get; set; }

    public DateTime? DateOfIssueOfBook { get; set; }

    public string? AccountNumber { get; set; }

    public DateTime? DateOfResignation { get; set; }

    public string? DecisionNumberOfResignation { get; set; }

    public decimal? Fines { get; set; }
}
