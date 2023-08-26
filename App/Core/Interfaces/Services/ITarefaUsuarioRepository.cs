namespace Core.Interfaces.Services;

public interface ITarefaUsuarioRepository
{
    Task CadastrarDinamicoAsync(TarefaUsuario tarefaUsuario = null, IEnumerable<TarefaUsuario> tarefasUsuarios = null);
}
