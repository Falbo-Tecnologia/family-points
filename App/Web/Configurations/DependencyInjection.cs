using Core.Interfaces.Repositories;
using Data.Repositories;

namespace Web.Configurations;

public static class DependencyInjection
{
    public static void AddDependencies(this IServiceCollection services, AppSetting appSetting)
    {
        services.AddSingleton(appSetting);

        services.AddControllersWithViews();

        services.AddScoped<AppDbContext>();

        services.AddScoped<IEncryption, Encryption>();
        services.AddScoped<ILogWriter, LogWriter>();
        services.AddScoped<INotification, Notification>();

        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
}
