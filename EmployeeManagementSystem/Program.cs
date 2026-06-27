using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── Database ─────────────────────────────────────────────────────────
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ── Identity ──────────────────────────────────────────────────────────
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength         = 6;
    options.Password.RequireDigit           = true;
    options.Password.RequireUppercase       = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(5);
    options.User.RequireUniqueEmail         = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// ── Auth cookie ───────────────────────────────────────────────────────
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath       = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan  = TimeSpan.FromHours(8);
});

// ── App Services ──────────────────────────────────────────────────────
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ── Seed database ─────────────────────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
    await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
}

app.Run();
