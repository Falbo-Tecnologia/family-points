namespace Core.Services;

public class TarefaUsuarioService : ITarefaUsuarioService
{
    private readonly ITarefaUsuarioRepository _tarefaUsuarioRepository;
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TarefaUsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task CadastrarDinamicoAsync(TarefaUsuario tarefa = null, IEnumerable<TarefaUsuario> tarefas = null)
    {
        await _tarefaUsuarioRepository.CadastrarDinamicoAsync(tarefa, tarefas);
    }
}
