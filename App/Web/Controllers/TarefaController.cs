namespace Web.Controllers;

[Route("tarefa")]
public class TarefaController : AuthenticatedController
{
    public readonly ITarefaRepository _tarefaRepository;
    public TarefaController(ITarefaRepository tarefaRepository)
    {
        _tarefaRepository = tarefaRepository;
    }

    public IActionResult Index() => View();

    [HttpPost("cadastrar")]
    public async Task<IActionResult> PostTarefasAsync(IEnumerable<Tarefa> tarefas)
    {
        if (!(tarefas.Count() > 0))
            return BadRequest("Nenhuma tarefa foi informada.");
        else
            await _tarefaRepository.PostTarefasAsync(UsuarioLogado.Id, tarefas);

        return Ok();
    }

    [HttpGet("tarefas")]
    public async Task<IActionResult> GetTarefasAsync()
    {
        var tarefas = await _tarefaRepository.GetTarefasAsync();
        return View("_ListaTarefas", tarefas);
    }
}
