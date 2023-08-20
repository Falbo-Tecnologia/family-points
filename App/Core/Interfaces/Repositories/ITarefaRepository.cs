namespace Core.Interfaces.Repositories;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> GetTarefasAsync();
    Task<Tarefa> GetTarefaAsync(int id);
    Task PostTarefasAsync(int idUsuarioLogado, IEnumerable<Tarefa> tarefas);
}
