namespace Data.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly AppDbContext _dbContext;

    public TipoUsuarioRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TipoUsuario>> ListarAsync() => await _dbContext.TiposUsuarios.ToListAsync();

    public async Task<TipoUsuario> BuscarAsync(int id) => await _dbContext.TiposUsuarios.FirstOrDefaultAsync(x => x.Id == id);
}
