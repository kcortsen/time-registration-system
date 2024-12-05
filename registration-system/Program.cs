using Microsoft.EntityFrameworkCore;
using SharedLibrary.Backend.BusinessLogic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(
        "Data Source=/Users/kristinacortsen/RiderProjects/registration-system/SharedLibrary/database.db"));

builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<CaseService>();
builder.Services.AddScoped<TimeRegistrationService>();

builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/MVC/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/MVC/Views/Shared/{0}.cshtml");
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();