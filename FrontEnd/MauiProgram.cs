using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Context;
using Data.Repositories;
using Data.UnitOfWork;
using Data.Interfaces;
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

        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped<IUserBusinessLogicLayer, UserBusinessLogicLayer>();
        builder.Services.AddScoped<IProductBusinessLogicLayer, ProductBusinessLogicLayer>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<CreateProductPage>();
        builder.Services.AddTransient<DashBoard>();
        builder.Services.AddTransient<BarOverview>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}