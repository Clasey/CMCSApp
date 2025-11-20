using CMCSApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Add Services
// -------------------------

// Add MVC Controllers with Views
builder.Services.AddControllersWithViews();

// Configure EF Core for SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add in-memory repository for demo or prototyping
builder.Services.AddSingleton<InMemoryRepository>();

// Access HttpContext in services/controllers
builder.Services.AddHttpContextAccessor();

// Configure cookie authentication
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect to login if not authenticated
        options.AccessDeniedPath = "/Account/Login"; // Redirect to login if access denied
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

// -------------------------
// Build App
// -------------------------
var app = builder.Build();

// -------------------------
// Middleware Pipeline
// -------------------------

// Error handling
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Must come before Authorization
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

// -------------------------
// Routing
// -------------------------

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Optional: Area routes if you have areas like Lecturer/Manager
// app.MapControllerRoute(
//     name: "areas",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
