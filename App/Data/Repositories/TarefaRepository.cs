
using Core.Interfaces.Repositories;
using Data.Server;

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

        public async Task PutTarefaAsync(Tarefa tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();
        }
    }
}
