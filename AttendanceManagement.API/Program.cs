using System.Reflection;
using System.Text;
using AttendanceManagement.API.Data;
using AttendanceManagement.API.Middleware;
using AttendanceManagement.API.Repositories.Implementations;
using AttendanceManagement.API.Repositories.Interfaces;
using AttendanceManagement.API.Services.Implementations;
using AttendanceManagement.API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// 1. KẾT NỐI CƠ SỞ DỮ LIỆU (TiDB CLOUD / MySQL)
// ============================================================================
// Lấy chuỗi kết nối từ biến môi trường (ConnectionStrings__DefaultConnection) hoặc appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Chưa cấu hình biến môi trường 'ConnectionStrings__DefaultConnection'.");
}

// Đăng ký AppDbContext vào bộ chứa Dependency Injection với MySQL / TiDB Cloud
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.11-mysql"), mySqlOptions =>
    {
        mySqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    }));

// ============================================================================
// 2. ĐĂNG KÝ DEPENDENCY INJECTION (REPOSITORIES & SERVICES)
// ============================================================================
// 2.1. Đăng ký tầng Repository (Truy xuất Database trực tiếp)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<IOvertimeRepository, OvertimeRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();

// 2.2. Đăng ký tầng Service (Xử lý Business Logic)
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IOvertimeService, OvertimeService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IReportService, ReportService>();

// ============================================================================
// 3. CẤU HÌNH XÁC THỰC BẰNG JWT BEARER TOKEN
// ============================================================================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var jwtSecret = jwtSettings["SecretKey"];
if (string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new InvalidOperationException("Chưa cấu hình biến môi trường 'JwtSettings__SecretKey'.");
}
var secretKey = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    // Thiết lập schema xác thực mặc định là Bearer Token
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Phù hợp cho môi trường dev local
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey), // Kiểm tra chữ ký hợp lệ
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"], // Kiểm tra máy chủ phát hành
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"], // Kiểm tra phía nhận token
        ValidateLifetime = true, // Kiểm tra hạn dùng của token
        ClockSkew = TimeSpan.Zero // Loại bỏ độ trễ thời gian mặc định (5 phút)
    };
});

// Kích hoạt tính năng phân quyền (Role-based Authorization)
builder.Services.AddAuthorization();

// ============================================================================
// 4. ĐĂNG KÝ CONTROLLERS VÀ SWAGGER UI & CORS
// ============================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Thông tin tài liệu API hiển thị trên giao diện Swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HRM Attendance Management API",
        Version = "v1",
        Description = "Hệ thống Web API Quản lý Nhân sự, Chấm công & Nghỉ phép (Phát triển bằng ASP.NET Core 9 Web API)",
        Contact = new OpenApiContact
        {
            Name = "Đội ngũ Phát triển Dự án HRM",
        }
    });

    // Tự động nạp tài liệu XML để Swagger hiển thị đầy đủ mô tả, chú thích của từng Endpoint và Model
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }

    // Cấu hình nút Authorize trên Swagger UI để test token JWT trực tiếp
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token JWT của bạn vào ô bên dưới (Swagger sẽ tự thêm tiền tố 'Bearer ' phía trước):\r\n\r\nVí dụ: eyJhbGciOiJIUzI1NiIsIn..."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// ============================================================================
// TỰ ĐỘNG KHỞI TẠO TÀI KHOẢN ADMIN VÀ DỮ LIỆU BAN ĐẦU (SEED DATA)
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        await DbInitializer.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Đã xảy ra lỗi khi khởi tạo tài khoản Admin ban đầu.");
    }
}

// ============================================================================
// 5. CẤU HÌNH HTTP REQUEST PIPELINE (MIDDLEWARES)
// ============================================================================
// Đăng ký Middleware bắt lỗi tập trung toàn hệ thống (đặt đầu pipeline)
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Bật Swagger UI cho cả môi trường Development và Production (Deploy)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "HRM Attendance Management API v1");
    options.RoutePrefix = "swagger";
    options.DocumentTitle = "HRM Attendance API - Tài liệu & Kiểm thử Swagger";
});

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

// QUAN TRỌNG: UseAuthentication PHẢI đứng trước UseAuthorization
app.UseAuthentication(); // 1. Nhận diện người dùng qua JWT Token
app.UseAuthorization();  // 2. Kiểm tra quyền truy cập (Admin, HR, Manager, Employee)

// Ánh xạ các Controller endpoints
app.MapControllers();

app.Run();
