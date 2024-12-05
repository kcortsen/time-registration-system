using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyMauiApp.ViewModels;
using MyMauiApp.Views;
using SharedLibrary.Backend.BusinessLogic;

namespace MyMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<EmployeeView>();
        builder.Services.AddTransient<EmployeeViewModel>();
        builder.Services.AddTransient<OverviewViewModel>();
        builder.Services.AddTransient<OverviewView>();
        builder.Services.AddScoped<TimeRegistrationService>();
        builder.Services.AddTransient<SummaryViewModel>();
        builder.Services.AddTransient<SummaryView>();
        builder.Services.AddTransient<CaseService>();
        builder.Services.AddTransient<CaseViewModel>();
        builder.Services.AddTransient<CaseView>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                "Data Source=/Users/kristinacortsen/RiderProjects/registration-system/SharedLibrary/database.db"));

        builder.Services.AddScoped<EmployeeService>();

        var app = builder.Build();
        ServiceProviderHelper.Initialize(app.Services);
        return app;

        return builder.Build();
    }
}