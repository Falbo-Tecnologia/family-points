namespace Data.Server;

public class BaseDbContext : DbContext
{
    private readonly DatabaseSetting _dbSetting;
    private readonly ILogWriter _logWriter;
    private readonly string _configurationFolder;

    public BaseDbContext(AppSetting appSetting, ILogWriter logWriter, string configurationFolder)
    {
        _dbSetting = appSetting.Database;
        _logWriter = logWriter;
        _configurationFolder = configurationFolder;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(GetConnectionString(), options =>
        {
            options.MaxBatchSize(100);
            options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        });

        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);

        if (Debugger.IsAttached || _dbSetting.EnableLog)
            optionsBuilder.EnableDetailedErrors().EnableSensitiveDataLogging();

        void LogAction(string log)
        {
            if (Debugger.IsAttached)
                _logWriter.ConsoleWrite(log, ConsoleColor.White);

            if (!string.IsNullOrEmpty(_dbSetting.PathLog))
                _logWriter.Write(_dbSetting.PathLog, log);
        }

        if (_dbSetting.EnableLog)
            optionsBuilder.LogTo(LogAction, LogLevel.Information, DbContextLoggerOptions.SingleLine);
        else
            optionsBuilder.LogTo(LogAction, LogLevel.Error);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;

        var executingAssembly = Assembly.GetExecutingAssembly();
        var filter = $"{executingAssembly.GetName().Name}.Configurations.{_configurationFolder}";

        modelBuilder.ApplyConfigurationsFromAssembly(
            executingAssembly,
            x => x.Namespace.Equals(filter)
        );
    }

    protected internal string GetConnectionString()
    {
        var uriString = _dbSetting.ConnectionString;
        var uri = new Uri(uriString);
        var db = uri.AbsolutePath.Trim('/');
        var user = uri.UserInfo.Split(':')[0];
        var passwd = uri.UserInfo.Split(':')[1];
        var port = uri.Port > 0 ? uri.Port : 5432;
        var connectionString = $"Server={uri.Host};Database={db};User Id={user};Password={passwd};Port={port}";
        return connectionString;
    }
}
