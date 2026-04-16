using BusinessLogic.BusinessLogicLayer;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FrontEnd;

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

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5433;Username=postgres;Password=1234;Database=appdb"));

        builder.Services.AddScoped<UserRepository>();
        builder.Services.AddScoped<ProductRepository>();

        builder.Services.AddScoped<UserBusinessLogicLayer>();
        builder.Services.AddScoped<ProductBusinessLogicLayer>();

        builder.Services.AddScoped<MainPage>();
        builder.Services.AddScoped<CreateProductPage>();
        builder.Services.AddScoped<DashBoard>();
        builder.Services.AddScoped<BarOverview>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}