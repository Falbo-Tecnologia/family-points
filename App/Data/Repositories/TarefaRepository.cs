namespace Data.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _dbContext;

        public TarefaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasAsync() => await _dbContext.Tarefas.ToListAsync();

        public async Task<Tarefa> GetTarefaAsync(int id) => await _dbContext.Tarefas.FirstOrDefaultAsync(x => x.Id == id);

        public async Task PostTarefasAsync(int idUsuarioLogado, IEnumerable<Tarefa> tarefas)
        {
            var tarefasTratadas = tarefas?.Select(x => new Tarefa
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                    Pontuacao = x.Pontuacao,
                    DataCadastro = DateTime.UtcNow,
                    UsuarioCadastro = idUsuarioLogado
                });
                
            if (tarefas.Count() > 1)
                await _dbContext.Tarefas.AddRangeAsync(tarefasTratadas);
            else
                await _dbContext.Tarefas.AddAsync(tarefasTratadas.FirstOrDefault());

            await _dbContext.SaveChangesAsync();
        }
    }
}
