using Core.Helpers;

namespace Web.Configurations;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, AppSetting appSetting)
    {
        services.AddSingleton(appSetting);

        services.AddControllersWithViews();

        services.AddScoped<Notification>();
        services.AddScoped<ILogWriter, LogWriter>();
        services.AddScoped<IEncryption, Encryption>();
        // services.AddScoped<AppDbContext>();
    }
}
