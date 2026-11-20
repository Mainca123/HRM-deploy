using System;
using System.Collections.Generic;

namespace AttendanceManagement.API.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string FullName { get; set; } = null!;

    public string? Alias { get; set; }

    public DateTime? BirthDate { get; set; }

    public bool? Sex { get; set; }

    public string? NationalityId { get; set; }

    public string? EthnicGroupId { get; set; }

    public string? ReligionId { get; set; }

    public string? MarriageStatusId { get; set; }

    public string? CultureLevel { get; set; }

    public DateTime? UnionEnrollingDate { get; set; }

    public string? UnionEnrollingPlace { get; set; }

    public DateTime? PartyEnrollingDate { get; set; }

    public string? PartyEnrollingPlace { get; set; }

    public DateTime? EnlistingDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    public string? Reason { get; set; }

    public string? Email { get; set; }

    public string? Photo { get; set; }

    public string? Notes { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<AnnualLeave> AnnualLeaves { get; set; } = new List<AnnualLeave>();

    public virtual ICollection<AuditMonthSalary> AuditMonthSalaries { get; set; } = new List<AuditMonthSalary>();

    public virtual ICollection<AuditTimeInTimeOut> AuditTimeInTimeOuts { get; set; } = new List<AuditTimeInTimeOut>();

    public virtual ICollection<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();

    public virtual ICollection<BeforeWorking> BeforeWorkings { get; set; } = new List<BeforeWorking>();

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; set; } = new List<EmployeeDepartment>();

    public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();

    public virtual Enrolling? Enrolling { get; set; }

    public virtual ICollection<Family> Families { get; set; } = new List<Family>();

    public virtual ICollection<ForeignLanguageLevel> ForeignLanguageLevels { get; set; } = new List<ForeignLanguageLevel>();

    public virtual Health? Health { get; set; }

    public virtual ICollection<HealthInsuranceCard> HealthInsuranceCards { get; set; } = new List<HealthInsuranceCard>();

    public virtual HealthStatus? HealthStatus { get; set; }

    public virtual Identification? Identification { get; set; }

    public virtual Military? Military { get; set; }

    public virtual ICollection<ModifiedDatum> ModifiedData { get; set; } = new List<ModifiedDatum>();

    public virtual ICollection<MonthAttendance> MonthAttendances { get; set; } = new List<MonthAttendance>();

    public virtual ICollection<MonthSalary> MonthSalaries { get; set; } = new List<MonthSalary>();

    public virtual ICollection<MonthWorking> MonthWorkings { get; set; } = new List<MonthWorking>();

    public virtual Nationality? Nationality { get; set; }

    public virtual Passport? Passport { get; set; }

    public virtual ICollection<PayrollMonthWorking> PayrollMonthWorkings { get; set; } = new List<PayrollMonthWorking>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    public virtual ICollection<ProfessionalLevel> ProfessionalLevels { get; set; } = new List<ProfessionalLevel>();

    public virtual ICollection<RegisterAbsence> RegisterAbsences { get; set; } = new List<RegisterAbsence>();

    public virtual ICollection<RegisterLateEarly> RegisterLateEarlies { get; set; } = new List<RegisterLateEarly>();

    public virtual ICollection<RegisterAttendanceItem> RegisterAttendanceItems { get; set; } = new List<RegisterAttendanceItem>();

    public virtual ICollection<RegisterNoAttendance> RegisterNoAttendances { get; set; } = new List<RegisterNoAttendance>();

    public virtual ICollection<RegisterOvertime> RegisterOvertimes { get; set; } = new List<RegisterOvertime>();

    public virtual ICollection<RegisterSalaryCategory> RegisterSalaryCategories { get; set; } = new List<RegisterSalaryCategory>();

    public virtual ICollection<RegisterSalaryItem> RegisterSalaryItems { get; set; } = new List<RegisterSalaryItem>();

    public virtual ICollection<RegisterShift> RegisterShifts { get; set; } = new List<RegisterShift>();

    public virtual Religion? Religion { get; set; }

    public virtual ICollection<Resignation> Resignations { get; set; } = new List<Resignation>();

    public virtual ICollection<Reward> Rewards { get; set; } = new List<Reward>();

    public virtual ICollection<SalaryProcess> SalaryProcesses { get; set; } = new List<SalaryProcess>();

    public virtual ICollection<SalaryTable> SalaryTables { get; set; } = new List<SalaryTable>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();

    public virtual SocialInsuranceBook? SocialInsuranceBook { get; set; }

    public virtual ICollection<TimeInTimeOut> TimeInTimeOuts { get; set; } = new List<TimeInTimeOut>();

    public virtual WorkingInitialization? WorkingInitialization { get; set; }
}
