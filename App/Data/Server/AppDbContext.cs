namespace Data.Server;

public class AppDbContext : BaseDbContext
{
    public AppDbContext(AppSetting appSetting, ILogWriter logWriter) : base(appSetting, logWriter, "ElephantSql")
    {

    }

    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
}