namespace Data.Repositories
{
    public class TarefaUsuarioRepository : ITarefaUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public TarefaUsuarioRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CadastrarDinamicoAsync(TarefaUsuario tarefaUsuario = null, IEnumerable<TarefaUsuario> tarefasUsuarios = null)
        {
            if (tarefaUsuario != null)
                await _dbContext.TarefasUsuarios.AddAsync(tarefaUsuario);

            if (tarefasUsuarios != null)
                await _dbContext.TarefasUsuarios.AddRangeAsync(tarefasUsuarios);

            await _dbContext.SaveChangesAsync();
        }
    }
}
