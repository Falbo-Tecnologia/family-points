namespace Core.Interfaces.Services;

public interface ITarefaUsuarioService
{
    Task CadastrarDinamicoAsync(TarefaUsuario tarefa = null, IEnumerable<TarefaUsuario> tarefas = null);
}
