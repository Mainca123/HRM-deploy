using System;
using System.Collections.Generic;
using AttendanceManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<A> As { get; set; }

    public virtual DbSet<Absence> Absences { get; set; }

    public virtual DbSet<AbsenceUnit> AbsenceUnits { get; set; }

    public virtual DbSet<AccessLog> AccessLogs { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<AnnualLeave> AnnualLeaves { get; set; }

    public virtual DbSet<AttendanceItem> AttendanceItems { get; set; }

    public virtual DbSet<AttendanceItemGroup> AttendanceItemGroups { get; set; }

    public virtual DbSet<AttendanceTable> AttendanceTables { get; set; }

    public virtual DbSet<AttendanceTableGroup> AttendanceTableGroups { get; set; }

    public virtual DbSet<AuditBreakOutBreakIn> AuditBreakOutBreakIns { get; set; }

    public virtual DbSet<AuditMonthSalary> AuditMonthSalaries { get; set; }

    public virtual DbSet<AuditSalaryTable> AuditSalaryTables { get; set; }

    public virtual DbSet<AuditTimeInTimeOut> AuditTimeInTimeOuts { get; set; }

    public virtual DbSet<AutoUpdate> AutoUpdates { get; set; }

    public virtual DbSet<B> Bs { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<BeforeWorking> BeforeWorkings { get; set; }

    public virtual DbSet<BreakOutBreakIn> BreakOutBreakIns { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CateringLog> CateringLogs { get; set; }

    public virtual DbSet<CommandButton> CommandButtons { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyType> CompanyTypes { get; set; }

    public virtual DbSet<Constant> Constants { get; set; }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Discipline> Disciplines { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Employee1> Employees1 { get; set; }

    public virtual DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

    public virtual DbSet<EmployeeInfo> EmployeeInfos { get; set; }

    public virtual DbSet<EmployeeInfoList> EmployeeInfoLists { get; set; }

    public virtual DbSet<EmployeeInfoListGroup> EmployeeInfoListGroups { get; set; }

    public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }

    public virtual DbSet<Enrolling> Enrollings { get; set; }

    public virtual DbSet<EthnicGroup> EthnicGroups { get; set; }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<FamilyType> FamilyTypes { get; set; }

    public virtual DbSet<FingerprintTemplate> FingerprintTemplates { get; set; }

    public virtual DbSet<ForeignLanguage> ForeignLanguages { get; set; }

    public virtual DbSet<ForeignLanguageLevel> ForeignLanguageLevels { get; set; }

    public virtual DbSet<FormOfPaying> FormOfPayings { get; set; }

    public virtual DbSet<Health> Healths { get; set; }

    public virtual DbSet<HealthInsuranceCard> HealthInsuranceCards { get; set; }

    public virtual DbSet<HealthStatus> HealthStatuses { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Identification> Identifications { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<LabourContractType> LabourContractTypes { get; set; }

    public virtual DbSet<Layout> Layouts { get; set; }

    public virtual DbSet<Leave> Leaves { get; set; }

    public virtual DbSet<MappingSalaryItem> MappingSalaryItems { get; set; }

    public virtual DbSet<MarriageStatus> MarriageStatuses { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Military> Militaries { get; set; }

    public virtual DbSet<ModifiedDatum> ModifiedData { get; set; }

    public virtual DbSet<MonthAttendance> MonthAttendances { get; set; }

    public virtual DbSet<MonthAttendance1> MonthAttendances1 { get; set; }

    public virtual DbSet<MonthSalary> MonthSalaries { get; set; }

    public virtual DbSet<MonthSalary1> MonthSalaries1 { get; set; }

    public virtual DbSet<MonthWorking> MonthWorkings { get; set; }

    public virtual DbSet<MonthWorking1> MonthWorkings1 { get; set; }

    public virtual DbSet<Nationality> Nationalities { get; set; }

    public virtual DbSet<OtherLevel> OtherLevels { get; set; }

    public virtual DbSet<OvertimeLimit> OvertimeLimits { get; set; }

    public virtual DbSet<PaidAbsence> PaidAbsences { get; set; }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<PaperType> PaperTypes { get; set; }

    public virtual DbSet<Passport> Passports { get; set; }

    public virtual DbSet<PayrollMonthAttendance> PayrollMonthAttendances { get; set; }

    public virtual DbSet<PayrollMonthWorking> PayrollMonthWorkings { get; set; }

    public virtual DbSet<Phone> Phones { get; set; }

    public virtual DbSet<PhoneType> PhoneTypes { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<ProfessionalLevel> ProfessionalLevels { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RawData1> RawData1s { get; set; }

    public virtual DbSet<RawDatum> RawData { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    public virtual DbSet<ReaderType> ReaderTypes { get; set; }

    public virtual DbSet<RegisterAbsence> RegisterAbsences { get; set; }

    public virtual DbSet<RegisterAttendanceItem> RegisterAttendanceItems { get; set; }

    public virtual DbSet<RegisterDayOff> RegisterDayOffs { get; set; }

    public virtual DbSet<RegisterLateEarly> RegisterLateEarlies { get; set; }

    public virtual DbSet<RegisterNoAttendance> RegisterNoAttendances { get; set; }

    public virtual DbSet<RegisterOvertime> RegisterOvertimes { get; set; }

    public virtual DbSet<RegisterSalaryCategory> RegisterSalaryCategories { get; set; }

    public virtual DbSet<RegisterSalaryItem> RegisterSalaryItems { get; set; }

    public virtual DbSet<RegisterShift> RegisterShifts { get; set; }

    public virtual DbSet<Religion> Religions { get; set; }

    public virtual DbSet<Resignation> Resignations { get; set; }

    public virtual DbSet<ResignationReason> ResignationReasons { get; set; }

    public virtual DbSet<Reward> Rewards { get; set; }

    public virtual DbSet<RightOfGroup> RightOfGroups { get; set; }

    public virtual DbSet<SalaryCategory> SalaryCategories { get; set; }

    public virtual DbSet<SalaryCode> SalaryCodes { get; set; }

    public virtual DbSet<SalaryItem> SalaryItems { get; set; }

    public virtual DbSet<SalaryItem1> SalaryItems1 { get; set; }

    public virtual DbSet<SalaryItemGroup> SalaryItemGroups { get; set; }

    public virtual DbSet<SalaryProcess> SalaryProcesses { get; set; }

    public virtual DbSet<SalaryTable> SalaryTables { get; set; }

    public virtual DbSet<SalaryTable1> SalaryTables1 { get; set; }

    public virtual DbSet<SalaryType> SalaryTypes { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<ShiftDetail> ShiftDetails { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SocialInsuranceBook> SocialInsuranceBooks { get; set; }

    public virtual DbSet<SpecialDay> SpecialDays { get; set; }

    public virtual DbSet<StandardWork> StandardWorks { get; set; }

    public virtual DbSet<TaxCode> TaxCodes { get; set; }

    public virtual DbSet<TimeInTimeOut> TimeInTimeOuts { get; set; }

    public virtual DbSet<TimeInTimeOut1> TimeInTimeOuts1 { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEmployee> UserEmployees { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<Vacation> Vacations { get; set; }

    public virtual DbSet<WorkingCycle> WorkingCycles { get; set; }

    public virtual DbSet<WorkingInitialization> WorkingInitializations { get; set; }

    public virtual DbSet<WorkingTimeRule> WorkingTimeRules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<A>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("A");

            entity.Property(e => e.AbsenceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AbsenceID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Absence>(entity =>
        {
            entity.ToTable("Absence");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Si).HasColumnName("SI");
        });

        modelBuilder.Entity<AbsenceUnit>(entity =>
        {
            entity.ToTable("AbsenceUnit");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AccessLog>(entity =>
        {
            entity.HasKey(e => new { e.WorkingDay, e.UserName });

            entity.ToTable("AccessLog");

            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(15);
            entity.Property(e => e.Macaddress)
                .HasMaxLength(50)
                .HasColumnName("MACAddress");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.AddressTypeId });

            entity.ToTable("Address");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AddressTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AddressTypeID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.AddressType).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AddressTypeId)
                .HasConstraintName("FK_Address_AddressType");

            entity.HasOne(d => d.Employee).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Address_Employee");
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.ToTable("AddressType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AnnualLeave>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Month, e.Year });

            entity.ToTable("AnnualLeave");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.Employee).WithMany(p => p.AnnualLeaves)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_AnnualLeave_Employee");
        });

        modelBuilder.Entity<AttendanceItem>(entity =>
        {
            entity.ToTable("AttendanceItem", tb =>
                {
                    tb.HasTrigger("OnDelete");
                    tb.HasTrigger("OnUpdate");
                });

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.AttendanceItemGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemGroupID");
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Value)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.AttendanceItemGroup).WithMany(p => p.AttendanceItems)
                .HasForeignKey(d => d.AttendanceItemGroupId)
                .HasConstraintName("FK_AttendanceItem_AttendanceItemGroup");
        });

        modelBuilder.Entity<AttendanceItemGroup>(entity =>
        {
            entity.ToTable("AttendanceItemGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ParentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ParentID");
        });

        modelBuilder.Entity<AttendanceTable>(entity =>
        {
            entity.ToTable("AttendanceTable");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.AttendanceTableGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceTableGroupID");
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.Display)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Position)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.AttendanceTableGroup).WithMany(p => p.AttendanceTables)
                .HasForeignKey(d => d.AttendanceTableGroupId)
                .HasConstraintName("FK_AttendanceTable_AttendanceTableGroup");
        });

        modelBuilder.Entity<AttendanceTableGroup>(entity =>
        {
            entity.ToTable("AttendanceTableGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ParentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ParentID");
        });

        modelBuilder.Entity<AuditBreakOutBreakIn>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.ShiftId, e.Iotimes });

            entity.ToTable("AuditBreakOutBreakIn");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.Iotimes).HasColumnName("IOTimes");
            entity.Property(e => e.BreakIn).HasColumnType("datetime");
            entity.Property(e => e.BreakOut).HasColumnType("datetime");
            entity.Property(e => e.ReasonId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReasonID");

            entity.HasOne(d => d.AuditTimeInTimeOut).WithMany(p => p.AuditBreakOutBreakIns)
                .HasForeignKey(d => new { d.EmployeeId, d.WorkingDay, d.ShiftId })
                .HasConstraintName("FK_AuditBreakOutBreakIn_AuditTimeInTimeOut");
        });

        modelBuilder.Entity<AuditMonthSalary>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("AuditMonthSalary");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.AuditMonthSalaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_AuditMonthSalary_Employee");
        });

        modelBuilder.Entity<AuditSalaryTable>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("AuditSalaryTable");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AuditTimeInTimeOut>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.ShiftId });

            entity.ToTable("AuditTimeInTimeOut");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.AuditTimeInTimeOuts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_AuditTimeInTimeOut_Employee");
        });

        modelBuilder.Entity<AutoUpdate>(entity =>
        {
            entity.HasKey(e => e.File);

            entity.ToTable("AutoUpdate");

            entity.Property(e => e.File).HasMaxLength(100);
        });

        modelBuilder.Entity<B>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("B");

            entity.Property(e => e.AbsenceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AbsenceID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Type).HasColumnType("decimal(18, 1)");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.ToTable("Bank");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.BankId });

            entity.ToTable("BankAccount");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.BankId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BankID");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Note).HasMaxLength(250);

            entity.HasOne(d => d.Employee).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BankAccount_Employee");
        });

        modelBuilder.Entity<BeforeWorking>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Organization });

            entity.ToTable("BeforeWorking");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Organization).HasMaxLength(50);
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.Result).HasMaxLength(200);

            entity.HasOne(d => d.Employee).WithMany(p => p.BeforeWorkings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_BeforeWorking_Employee");
        });

        modelBuilder.Entity<BreakOutBreakIn>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.ShiftId, e.Iotimes });

            entity.ToTable("BreakOutBreakIn");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.Iotimes).HasColumnName("IOTimes");
            entity.Property(e => e.BreakIn).HasColumnType("datetime");
            entity.Property(e => e.BreakOut).HasColumnType("datetime");
            entity.Property(e => e.ReasonId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReasonID");

            entity.HasOne(d => d.TimeInTimeOut).WithMany(p => p.BreakOutBreakIns)
                .HasForeignKey(d => new { d.EmployeeId, d.WorkingDay, d.ShiftId })
                .HasConstraintName("FK_BreakOutBreakIn_TimeInTimeOut");
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("Card");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Cards)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Card_Employee");
        });

        modelBuilder.Entity<CateringLog>(entity =>
        {
            entity.HasKey(e => new { e.EnrollNumber, e.SwipedDate });

            entity.ToTable("CateringLog");

            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SwipedDate).HasColumnType("datetime");
            entity.Property(e => e.ReaderId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ReaderID");
        });

        modelBuilder.Entity<CommandButton>(entity =>
        {
            entity.ToTable("CommandButton");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Owner)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Text).HasMaxLength(100);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.ToTable("Company");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CompanyTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CompanyTypeID");
            entity.Property(e => e.Default).HasDefaultValue(true);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Website)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CompanyType).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CompanyTypeId)
                .HasConstraintName("FK_Company_CompanyType");
        });

        modelBuilder.Entity<CompanyType>(entity =>
        {
            entity.ToTable("CompanyType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("NameEN");
        });

        modelBuilder.Entity<Constant>(entity =>
        {
            entity.ToTable("Constant");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("ntext");
        });

        modelBuilder.Entity<Datum>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Idcard)
                .HasMaxLength(255)
                .HasColumnName("IDCard");
            entity.Property(e => e.Idm).HasColumnName("IDM");
            entity.Property(e => e.ThoiGian).HasColumnType("datetime");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department", tb =>
                {
                    tb.HasTrigger("DeleteRightOfGroup");
                    tb.HasTrigger("UpdateRightOfGroup");
                });

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.ParentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ParentID");
        });

        modelBuilder.Entity<Discipline>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.No });

            entity.ToTable("Discipline");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Duration).HasColumnType("datetime");
            entity.Property(e => e.Form).HasMaxLength(100);
            entity.Property(e => e.Notes).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Disciplines)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Discipline_Employee");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.Code)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CultureLevel).HasMaxLength(250);
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnlistingDate).HasColumnType("datetime");
            entity.Property(e => e.EthnicGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EthnicGroupID");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.MarriageStatusId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MarriageStatusID");
            entity.Property(e => e.NationalityId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("NationalityID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PartyEnrollingDate).HasColumnType("datetime");
            entity.Property(e => e.PartyEnrollingPlace).HasMaxLength(100);
            entity.Property(e => e.Photo).HasMaxLength(250);
            entity.Property(e => e.Reason).HasMaxLength(100);
            entity.Property(e => e.ReligionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReligionID");
            entity.Property(e => e.UnionEnrollingDate).HasColumnType("datetime");
            entity.Property(e => e.UnionEnrollingPlace).HasMaxLength(100);

            entity.HasOne(d => d.Nationality).WithMany(p => p.Employees)
                .HasForeignKey(d => d.NationalityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_Nationality");

            entity.HasOne(d => d.Religion).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ReligionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Employee_Religion");
        });

        modelBuilder.Entity<Employee1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.Id, e.WorkingDay });

            entity.ToTable("Employee_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.Code)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.PositionName).HasMaxLength(50);
        });

        modelBuilder.Entity<EmployeeDepartment>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("EmployeeDepartment", tb => tb.HasTrigger("ChangeDepartmentID"));

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Department).WithMany(p => p.EmployeeDepartments)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_EmployeeDepartment_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeDepartments)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeeDepartment_Employee");
        });

        modelBuilder.Entity<EmployeeInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EmployeeInfo");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.BirthPlace).HasMaxLength(100);
            entity.Property(e => e.BookNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CardFromDate).HasColumnType("datetime");
            entity.Property(e => e.CardId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CardID");
            entity.Property(e => e.Code)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CultureLevel).HasMaxLength(250);
            entity.Property(e => e.CurrentAddress).HasMaxLength(100);
            entity.Property(e => e.DateOfIssueOfBook).HasColumnType("datetime");
            entity.Property(e => e.DateOfIssueOfCode).HasColumnType("datetime");
            entity.Property(e => e.DateOfResignation).HasColumnType("datetime");
            entity.Property(e => e.DecisionNumberOfResignation).HasMaxLength(50);
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EthnicGroupName).HasMaxLength(20);
            entity.Property(e => e.Fines).HasColumnType("money");
            entity.Property(e => e.FirstDependent).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(3);
            entity.Property(e => e.Home)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.InitialDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedPlace).HasMaxLength(50);
            entity.Property(e => e.JobId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("JobID");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.MaritalStatus).HasMaxLength(20);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NationalityName).HasMaxLength(20);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.Office)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Photo).HasMaxLength(250);
            entity.Property(e => e.PositionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PositionID");
            entity.Property(e => e.PositionName).HasMaxLength(50);
            entity.Property(e => e.SecondDependent).HasMaxLength(30);
            entity.Property(e => e.SigningDate).HasColumnType("datetime");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EmployeeInfoList>(entity =>
        {
            entity.HasKey(e => e.FieldName);

            entity.ToTable("EmployeeInfoList");

            entity.Property(e => e.FieldName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Caption).HasMaxLength(50);
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.EmployeeInfoListGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("EmployeeInfoListGroupID");
        });

        modelBuilder.Entity<EmployeeInfoListGroup>(entity =>
        {
            entity.ToTable("EmployeeInfoListGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ParentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ParentID");
        });

        modelBuilder.Entity<EmployeePosition>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("EmployeePosition");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.JobId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("JobID");
            entity.Property(e => e.PositionId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PositionID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePositions)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmployeePosition_Employee");
        });

        modelBuilder.Entity<Enrolling>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Enrolling");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.PartyEnrollingDate).HasColumnType("datetime");
            entity.Property(e => e.PartyEnrollingPlace).HasMaxLength(50);
            entity.Property(e => e.UnionEnrollingDate).HasColumnType("datetime");
            entity.Property(e => e.UnionEnrollingPlace).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithOne(p => p.Enrolling)
                .HasForeignKey<Enrolling>(d => d.EmployeeId)
                .HasConstraintName("FK_Enrolling_Employee");
        });

        modelBuilder.Entity<EthnicGroup>(entity =>
        {
            entity.ToTable("EthnicGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Family>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FamilyTypeId, e.Ordinal });

            entity.ToTable("Family");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FamilyTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("FamilyTypeID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(30);
            entity.Property(e => e.Occupation).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Families)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Family_Employee");

            entity.HasOne(d => d.FamilyType).WithMany(p => p.Families)
                .HasForeignKey(d => d.FamilyTypeId)
                .HasConstraintName("FK_Family_FamilyType");
        });

        modelBuilder.Entity<FamilyType>(entity =>
        {
            entity.ToTable("FamilyType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<FingerprintTemplate>(entity =>
        {
            entity.HasKey(e => new { e.EnrollNumber, e.Index }).HasName("PK_FingerTemplate");

            entity.ToTable("FingerprintTemplate");

            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ForeignLanguage>(entity =>
        {
            entity.ToTable("ForeignLanguage");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<ForeignLanguageLevel>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.ForeignLanguageId }).HasName("PK_EmployeeForeignLanguageLevel");

            entity.ToTable("ForeignLanguageLevel");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ForeignLanguageId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ForeignLanguageID");
            entity.Property(e => e.Level)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.ForeignLanguageLevels)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ForeignLanguageLevel_Employee");
        });

        modelBuilder.Entity<FormOfPaying>(entity =>
        {
            entity.ToTable("FormOfPaying");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Health>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK_EmployeeHealth");

            entity.ToTable("Health");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Allergy).HasMaxLength(100);
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Defect).HasMaxLength(100);
            entity.Property(e => e.Disease).HasMaxLength(100);
            entity.Property(e => e.HealthStatusId).HasColumnName("HealthStatusID");
            entity.Property(e => e.Other).HasMaxLength(100);
            entity.Property(e => e.Vaccination).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithOne(p => p.Health)
                .HasForeignKey<Health>(d => d.EmployeeId)
                .HasConstraintName("FK_Health_Employee");
        });

        modelBuilder.Entity<HealthInsuranceCard>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("HealthInsuranceCard");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.HospitalId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("HospitalID");
            entity.Property(e => e.Number)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProvinceId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ProvinceID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.HealthInsuranceCards)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_HealthInsuranceCard_Employee");
        });

        modelBuilder.Entity<HealthStatus>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("HealthStatus");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.BloodGroup)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.LeftVision)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.RightVision)
                .HasMaxLength(5)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.HealthStatus)
                .HasForeignKey<HealthStatus>(d => d.EmployeeId)
                .HasConstraintName("FK_HealthStatus_Employee");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.ToTable("Hospital");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Identification>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Identification");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.IssuedDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedPlace).HasMaxLength(50);
            entity.Property(e => e.No)
                .HasMaxLength(12)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.Identification)
                .HasForeignKey<Identification>(d => d.EmployeeId)
                .HasConstraintName("FK_Identification_Employee");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("Job");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<LabourContractType>(entity =>
        {
            entity.ToTable("LabourContractType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Layout>(entity =>
        {
            entity.HasKey(e => new { e.Item, e.Month, e.Year, e.Language });

            entity.ToTable("Layout");

            entity.Property(e => e.Item)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("ntext");
        });

        modelBuilder.Entity<Leave>(entity =>
        {
            entity.ToTable("Leave");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<MappingSalaryItem>(entity =>
        {
            entity.ToTable("MappingSalaryItem");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
        });

        modelBuilder.Entity<MarriageStatus>(entity =>
        {
            entity.ToTable("MarriageStatus");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Owner)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Military>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Military");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.BeginDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithOne(p => p.Military)
                .HasForeignKey<Military>(d => d.EmployeeId)
                .HasConstraintName("FK_Military_Employee");
        });

        modelBuilder.Entity<ModifiedDatum>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.SwipedDate }).HasName("PK_ModifiedData_1");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.SwipedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Value).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.ModifiedData)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ModifiedData_Employee");
        });

        modelBuilder.Entity<MonthAttendance>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.AttendanceItemId });

            entity.ToTable("MonthAttendance");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Text)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.MonthAttendances)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_MonthAttendance_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.MonthAttendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_MonthAttendance_Employee");
        });

        modelBuilder.Entity<MonthAttendance1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.EmployeeId, e.WorkingDay, e.AttendanceItemId });

            entity.ToTable("MonthAttendance_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Text)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<MonthSalary>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("MonthSalary");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.MonthSalaries)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MonthSalary_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.MonthSalaries)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_MonthSalary_Employee");
        });

        modelBuilder.Entity<MonthSalary1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("MonthSalary_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MonthWorking>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.AttendanceItemId });

            entity.ToTable("MonthWorking", tb => tb.HasTrigger("DeleteAttendanceItemID"));

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Department).WithMany(p => p.MonthWorkings)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_MonthWorking_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.MonthWorkings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_MonthWorking_Employee");
        });

        modelBuilder.Entity<MonthWorking1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.EmployeeId, e.FromDate, e.AttendanceItemId });

            entity.ToTable("MonthWorking_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Nationality>(entity =>
        {
            entity.ToTable("Nationality");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<OtherLevel>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Category });

            entity.ToTable("OtherLevel");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Category).HasMaxLength(20);
            entity.Property(e => e.Details).HasMaxLength(100);
        });

        modelBuilder.Entity<OvertimeLimit>(entity =>
        {
            entity.HasKey(e => new { e.ShiftId, e.ReplacementShiftId, e.FromDate });

            entity.ToTable("OvertimeLimit");

            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.ReplacementShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReplacementShiftID");
            entity.Property(e => e.EmployeeIds)
                .IsUnicode(false)
                .HasColumnName("EmployeeIDs");
            entity.Property(e => e.FromTime).HasColumnType("datetime");
            entity.Property(e => e.ToTime).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 1)");
        });

        modelBuilder.Entity<PaidAbsence>(entity =>
        {
            entity.ToTable("PaidAbsence");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SortOrder)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paper>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.PaperTypeId });

            entity.ToTable("Paper");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("EmployeeID");
            entity.Property(e => e.PaperTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PaperTypeID");
            entity.Property(e => e.Notes).HasMaxLength(250);
        });

        modelBuilder.Entity<PaperType>(entity =>
        {
            entity.ToTable("PaperType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Passport>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Passport");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedDate).HasColumnType("datetime");
            entity.Property(e => e.IssuedPlace).HasMaxLength(50);
            entity.Property(e => e.No)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.Passport)
                .HasForeignKey<Passport>(d => d.EmployeeId)
                .HasConstraintName("FK_Visa_Employee");
        });

        modelBuilder.Entity<PayrollMonthAttendance>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.AttendanceItemId });

            entity.ToTable("PayrollMonthAttendance");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Text)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PayrollMonthWorking>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.AttendanceItemId });

            entity.ToTable("PayrollMonthWorking");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.PayrollMonthWorkings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_PayrollMonthWorking_Employee");
        });

        modelBuilder.Entity<Phone>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.PhoneTypeId });

            entity.ToTable("Phone");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.PhoneTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("PhoneTypeID");
            entity.Property(e => e.Number)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Phones)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Phone_Employee");

            entity.HasOne(d => d.PhoneType).WithMany(p => p.Phones)
                .HasForeignKey(d => d.PhoneTypeId)
                .HasConstraintName("FK_Phone_PhoneType");
        });

        modelBuilder.Entity<PhoneType>(entity =>
        {
            entity.ToTable("PhoneType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Position");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NameE).HasMaxLength(50);
        });

        modelBuilder.Entity<ProfessionalLevel>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Order });

            entity.ToTable("ProfessionalLevel");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.ProfessionalLevels)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ProfessionalLevel_Employee");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<RawData1>(entity =>
        {
            entity.HasKey(e => new { e.EnrollNumber, e.SwipedDate });

            entity.ToTable("RawData1");

            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SwipedDate).HasColumnType("datetime");
            entity.Property(e => e.ReaderId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ReaderID");
        });

        modelBuilder.Entity<RawDatum>(entity =>
        {
            entity.HasKey(e => new { e.EnrollNumber, e.SwipedDate });

            entity.ToTable(tb => tb.HasTrigger("GetTimeInTimeOut"));

            entity.Property(e => e.EnrollNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SwipedDate).HasColumnType("datetime");
            entity.Property(e => e.ReaderId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("ReaderID");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.ToTable("Reader");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CommPort)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CommType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LastRecord).HasColumnType("datetime");
            entity.Property(e => e.LastTime).HasColumnType("datetime");
            entity.Property(e => e.ReaderTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ReaderTypeID");
        });

        modelBuilder.Entity<ReaderType>(entity =>
        {
            entity.ToTable("ReaderType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RegisterAbsence>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.AbsenceId });

            entity.ToTable("RegisterAbsence", tb =>
                {
                    tb.HasTrigger("DeleteDurationAbsenceIDForDelete");
                    tb.HasTrigger("DeleteDurationAbsenceIDForUpdate");
                    tb.HasTrigger("UpdateAnnualLeave");
                });

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.AbsenceId)
                .HasMaxLength(10)
                .HasColumnName("AbsenceID");
            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.ApprovedUserName).HasMaxLength(15);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Unit).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Absence).WithMany(p => p.RegisterAbsences)
                .HasForeignKey(d => d.AbsenceId)
                .HasConstraintName("FK_RegisterAbsence_Absence");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterAbsences)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterAbsence_Employee");
        });

        modelBuilder.Entity<RegisterAttendanceItem>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.AttendanceItemId });

            entity.ToTable("RegisterAttendanceItem");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.AttendanceItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("AttendanceItemID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Text)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.AttendanceItem).WithMany(p => p.RegisterAttendanceItems)
                .HasForeignKey(d => d.AttendanceItemId)
                .HasConstraintName("FK_RegisterAttendanceItem_AttendanceItem");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterAttendanceItems)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterAttendanceItem_Employee");
        });

        modelBuilder.Entity<RegisterDayOff>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("RegisterDayOff");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RegisterLateEarly>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.Type });

            entity.ToTable("RegisterLateEarly");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Reason).HasMaxLength(250);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<RegisterNoAttendance>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("RegisterNoAttendance");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterNoAttendances)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterNoAttendance_Employee");
        });

        modelBuilder.Entity<RegisterOvertime>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.Type });

            entity.ToTable("RegisterOvertime");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 1)");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterOvertimes)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterOvertime_Employee");
        });

        modelBuilder.Entity<RegisterSalaryCategory>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("RegisterSalaryCategory");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.SalaryCategoryId)
                .HasMaxLength(10)
                .HasColumnName("SalaryCategoryID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterSalaryCategories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterSalaryCategory_Employee");
        });

        modelBuilder.Entity<RegisterSalaryItem>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("RegisterSalaryItem", tb =>
                {
                    tb.HasTrigger("DeleteRegisterSalaryItemIDForDelete");
                    tb.HasTrigger("DeleteRegisterSalaryItemIDForUpdate");
                });

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterSalaryItems)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterSalaryItem_Employee");
        });

        modelBuilder.Entity<RegisterShift>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("RegisterShift");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserName).HasMaxLength(15);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedUserName).HasMaxLength(15);
            entity.Property(e => e.ShiftId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ShiftID");

            entity.HasOne(d => d.Employee).WithMany(p => p.RegisterShifts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_RegisterShift_Employee");
        });

        modelBuilder.Entity<Religion>(entity =>
        {
            entity.ToTable("Religion");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Resignation>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("Resignation", tb =>
                {
                    tb.HasTrigger("DeleteEmployeeID");
                    tb.HasTrigger("DeleteWorkByUpdate");
                    tb.HasTrigger("RestoreEmployeeID");
                });

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.Allowance).HasColumnType("money");
            entity.Property(e => e.ContractIndemnity).HasColumnType("money");
            entity.Property(e => e.Decision).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(250);
            entity.Property(e => e.Other).HasColumnType("money");
            entity.Property(e => e.ResignationReasonId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ResignationReasonID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Resignations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Resignation_Employee");
        });

        modelBuilder.Entity<ResignationReason>(entity =>
        {
            entity.ToTable("ResignationReason");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Reward>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.No });

            entity.ToTable("Reward");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.No)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.Organization).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Rewards)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Reward_Employee");
        });

        modelBuilder.Entity<RightOfGroup>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.FunctionId, e.FunctionType });

            entity.ToTable("RightOfGroup");

            entity.Property(e => e.GroupId)
                .HasMaxLength(15)
                .HasColumnName("GroupID");
            entity.Property(e => e.FunctionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FunctionID");

            entity.HasOne(d => d.Group).WithMany(p => p.RightOfGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_RightOfGroup_UserGroup");
        });

        modelBuilder.Entity<SalaryCategory>(entity =>
        {
            entity.ToTable("SalaryCategory");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SalaryCode>(entity =>
        {
            entity.ToTable("SalaryCode");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Value).HasColumnType("decimal(18, 1)");
        });

        modelBuilder.Entity<SalaryItem>(entity =>
        {
            entity.ToTable("SalaryItem");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.Display)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.SalaryItemGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemGroupID");
            entity.Property(e => e.Value)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.SalaryItemGroup).WithMany(p => p.SalaryItems)
                .HasForeignKey(d => d.SalaryItemGroupId)
                .HasConstraintName("FK_SalaryItem_SalaryItemGroup");
        });

        modelBuilder.Entity<SalaryItem1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.Id });

            entity.ToTable("SalaryItem_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.DataType).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.SalaryItemGroupId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemGroupID");
            entity.Property(e => e.Value)
                .HasMaxLength(1000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SalaryItemGroup>(entity =>
        {
            entity.ToTable("SalaryItemGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ParentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ParentID");
        });

        modelBuilder.Entity<SalaryProcess>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate });

            entity.ToTable("SalaryProcess");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.BasicSalary).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.DecisionId)
                .HasMaxLength(20)
                .HasColumnName("DecisionID");
            entity.Property(e => e.InsuranceSalary).HasColumnType("decimal(18, 1)");
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.SalaryTypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryTypeID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryProcesses)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_SalaryProcess_Employee");
        });

        modelBuilder.Entity<SalaryTable>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("SalaryTable");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.SalaryTables)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SalaryTable_Department");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryTables)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_SalaryTable_Employee");
        });

        modelBuilder.Entity<SalaryTable1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.EmployeeId, e.FromDate, e.SalaryItemId });

            entity.ToTable("SalaryTable_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SalaryItemId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SalaryItemID");
            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SalaryType>(entity =>
        {
            entity.ToTable("SalaryType");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.ToTable("Shift");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<ShiftDetail>(entity =>
        {
            entity.HasKey(e => new { e.ShiftId, e.FromDate });

            entity.ToTable("ShiftDetail");

            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.NormalBreakIn).HasColumnType("datetime");
            entity.Property(e => e.NormalBreakOut).HasColumnType("datetime");
            entity.Property(e => e.NormalTimeIn).HasColumnType("datetime");
            entity.Property(e => e.NormalTimeOut).HasColumnType("datetime");
            entity.Property(e => e.SaturdayBreakIn).HasColumnType("datetime");
            entity.Property(e => e.SaturdayBreakOut).HasColumnType("datetime");
            entity.Property(e => e.SaturdayTimeIn).HasColumnType("datetime");
            entity.Property(e => e.SaturdayTimeOut).HasColumnType("datetime");
            entity.Property(e => e.SundayBreakIn).HasColumnType("datetime");
            entity.Property(e => e.SundayBreakOut).HasColumnType("datetime");
            entity.Property(e => e.SundayTimeIn).HasColumnType("datetime");
            entity.Property(e => e.SundayTimeOut).HasColumnType("datetime");
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.Shift).WithMany(p => p.ShiftDetails)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_ShiftDetail_Shift");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.Duration });

            entity.ToTable("Skill");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Diploma).HasMaxLength(50);
            entity.Property(e => e.Organization).HasMaxLength(50);
            entity.Property(e => e.Terms).HasMaxLength(100);

            entity.HasOne(d => d.Employee).WithMany(p => p.Skills)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_Skill_Employee");
        });

        modelBuilder.Entity<SocialInsuranceBook>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("SocialInsuranceBook");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.DateOfIssue).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithOne(p => p.SocialInsuranceBook)
                .HasForeignKey<SocialInsuranceBook>(d => d.EmployeeId)
                .HasConstraintName("FK_SocialInsuranceBook_Employee");
        });

        modelBuilder.Entity<SpecialDay>(entity =>
        {
            entity.HasKey(e => new { e.WorkingDay, e.Type }).HasName("PK_SpecialDay_1");

            entity.ToTable("SpecialDay");
        });

        modelBuilder.Entity<StandardWork>(entity =>
        {
            entity.HasKey(e => e.Value);

            entity.ToTable("StandardWork");

            entity.Property(e => e.Value).HasColumnType("decimal(18, 1)");
        });

        modelBuilder.Entity<TaxCode>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("TaxCode");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DateOfIssue).HasColumnType("datetime");
            entity.Property(e => e.FirstDependent).HasMaxLength(30);
            entity.Property(e => e.SecondDependent).HasMaxLength(30);
        });

        modelBuilder.Entity<TimeInTimeOut>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.WorkingDay, e.ShiftId });

            entity.ToTable("TimeInTimeOut", tb =>
                {
                    tb.HasTrigger("ClassifyWork");
                    tb.HasTrigger("DeleteWork");
                });

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.TimeInTimeOuts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_TimeInTimeOut_Employee");

            entity.HasOne(d => d.Shift).WithMany(p => p.TimeInTimeOuts)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_TimeInTimeOut_Shift");
        });

        modelBuilder.Entity<TimeInTimeOut1>(entity =>
        {
            entity.HasKey(e => new { e.Spid, e.EmployeeId, e.WorkingDay, e.ShiftId });

            entity.ToTable("TimeInTimeOut_");

            entity.Property(e => e.Spid).HasColumnName("SPID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.WorkingDay).HasColumnType("datetime");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.TimeIn).HasColumnType("datetime");
            entity.Property(e => e.TimeOut).HasColumnType("datetime");
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => new { e.Item, e.Language });

            entity.ToTable("Title");

            entity.Property(e => e.Item)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Language).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("User");

            entity.Property(e => e.Name).HasMaxLength(15);
            entity.Property(e => e.GroupId)
                .HasMaxLength(15)
                .HasColumnName("GroupID");
            entity.Property(e => e.Password).HasMaxLength(128);
            entity.Property(e => e.TypeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("TypeID");

            entity.HasOne(d => d.Group).WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_UserGroup");
        });

        modelBuilder.Entity<UserEmployee>(entity =>
        {
            entity.HasKey(e => e.UserName);

            entity.ToTable("UserEmployee");

            entity.Property(e => e.UserName).HasMaxLength(15);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.EmployeeIds).HasColumnName("EmployeeIDs");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.ToTable("UserGroup");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(15);
        });

        modelBuilder.Entity<Vacation>(entity =>
        {
            entity.ToTable("Vacation");

            entity.Property(e => e.Id)
                .HasColumnType("datetime")
                .HasColumnName("ID");
            entity.Property(e => e.EmployeeIds)
                .IsUnicode(false)
                .HasColumnName("EmployeeIDs");
            entity.Property(e => e.Notes).HasMaxLength(50);
        });

        modelBuilder.Entity<WorkingCycle>(entity =>
        {
            entity.HasKey(e => new { e.DepartmentId, e.Month, e.Year });

            entity.ToTable("WorkingCycle");

            entity.Property(e => e.DepartmentId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DepartmentID");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<WorkingInitialization>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("WorkingInitialization");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.BySource).HasMaxLength(50);
            entity.Property(e => e.InitialDate).HasColumnType("datetime");
            entity.Property(e => e.Interviewer).HasMaxLength(30);
            entity.Property(e => e.PresentPerson).HasMaxLength(30);
            entity.Property(e => e.RecruitingOfficer).HasMaxLength(30);
            entity.Property(e => e.SigningDate).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithOne(p => p.WorkingInitialization)
                .HasForeignKey<WorkingInitialization>(d => d.EmployeeId)
                .HasConstraintName("FK_WorkingInitialization_Employee");
        });

        modelBuilder.Entity<WorkingTimeRule>(entity =>
        {
            entity.HasKey(e => new { e.FromTimeIn, e.ToTimeIn, e.FromTimeOut, e.ToTimeOut });

            entity.ToTable("WorkingTimeRule");

            entity.Property(e => e.FromTimeIn).HasColumnType("datetime");
            entity.Property(e => e.ToTimeIn).HasColumnType("datetime");
            entity.Property(e => e.FromTimeOut).HasColumnType("datetime");
            entity.Property(e => e.ToTimeOut).HasColumnType("datetime");
            entity.Property(e => e.ShiftId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShiftID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
