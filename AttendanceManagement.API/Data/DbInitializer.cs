using AttendanceManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagement.API.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        /* 
        // ====================================================================
        // GHI CHÚ: TẠM COMMENT ĐOẠN TẠO GROUP VÌ DATABASE ĐÃ CÓ SẴN CÁC NHÓM GỐC:
        // ID '1' = Administration (Quản trị)
        // ID '2' = Chấm Công
        // ID '3' = Tiền lương
        // ID '4' = Nhân sự (HR)
        // ID '5' = Nhà ăn
        // ID '6' = XN1 (Quản lý)
        // ID '8' = XN3 (Quản lý)
        // ====================================================================

        if (!await context.UserGroups.AnyAsync(g => g.Id == "ADMIN"))
        {
            context.UserGroups.Add(new UserGroup { Id = "ADMIN", Name = "Quản trị viên", Description = "Toàn quyền quản trị hệ thống" });
        }
        if (!await context.UserGroups.AnyAsync(g => g.Id == "HR"))
        {
            context.UserGroups.Add(new UserGroup { Id = "HR", Name = "Nhân sự", Description = "Quản lý hồ sơ nhân viên và công lương" });
        }
        if (!await context.UserGroups.AnyAsync(g => g.Id == "MANAGER"))
        {
            context.UserGroups.Add(new UserGroup { Id = "MANAGER", Name = "Quản lý", Description = "Trưởng bộ phận phê duyệt đơn từ" });
        }
        if (!await context.UserGroups.AnyAsync(g => g.Id == "EMPLOYEE"))
        {
            context.UserGroups.Add(new UserGroup { Id = "EMPLOYEE", Name = "Nhân viên", Description = "Nhân viên cơ bản" });
        }
        await context.SaveChangesAsync();
        */

        // ====================================================================
        // KHỞI TẠO TÀI KHOẢN ADMIN BAN ĐẦU (NẾU ĐÃ CÓ THÌ BỎ QUA, CHƯA CÓ MỚI TẠO)
        // ====================================================================

        // 1. Kiểm tra tài khoản "admin" đã tồn tại trong bảng User chưa
        bool adminExists = await context.Users.AnyAsync(u => u.Name == "admin");
        if (adminExists)
        {
            // Đã có tài khoản admin rồi thì bỏ qua, không làm gì cả
            return;
        }

        // 2. Nếu chưa có, đảm bảo có 1 hồ sơ Employee gắn với tài khoản quản trị
        var adminEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Code == "ADMIN");
        if (adminEmployee == null)
        {
            // Tìm Max ID hiện tại trong bảng Employee để gán ID tiếp theo an toàn
            int nextId = await context.Employees.AnyAsync()
                ? (await context.Employees.MaxAsync(e => e.Id)) + 1
                : 1;

            adminEmployee = new Employee
            {
                Id = nextId,
                Code = "ADMIN",
                FullName = "Quản Trị Viên Hệ Thống"
            };
            context.Employees.Add(adminEmployee);
            await context.SaveChangesAsync();
        }

        // 3. Tạo tài khoản "admin" thuộc nhóm quyền Quản trị gốc (GroupId = "1" - Administration)
        var newAdminUser = new User
        {
            Name = "admin",
            Password = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            GroupId = "1", // Gắn với nhóm gốc: 1 = Administration
            TypeId = "1",
            AuditData = 0,
            Permission = true
        };
        context.Users.Add(newAdminUser);
        await context.SaveChangesAsync();

        // 4. Tạo liên kết trong bảng UserEmployee nếu chưa có
        bool userEmployeeExists = await context.UserEmployees.AnyAsync(ue => ue.UserName == "admin");
        if (!userEmployeeExists)
        {
            var newUserEmployee = new UserEmployee
            {
                UserName = "admin",
                EmployeeId = adminEmployee.Id
            };
            context.UserEmployees.Add(newUserEmployee);
            await context.SaveChangesAsync();
        }
    }
}

