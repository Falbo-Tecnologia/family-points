namespace Web.Controllers;

[Route("usuario")]
public class UsuarioController : Controller
{
    private readonly INotification _notification;
    private readonly IUsuarioService _usuarioService;
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioService usuarioService, INotification notification, IUsuarioRepository usuarioRepository)
    {
        _usuarioService = usuarioService;
        _notification = notification;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IActionResult> Index() => View("Index", await _usuarioRepository.ListarAsync());

    [HttpGet("cadastro-usuario")]
    public IActionResult GetViewCadastro() => View("_CadastroUsuario");

    [HttpPost("cadastro-usuario")]
    public async Task<IActionResult> PostCadastroAsync(Usuario usuario)
    {
        if (usuario == null)
            return BadRequest("Não foi possível acessar os dados do usuário");

        if (!usuario.IsValid(_notification))
            return BadRequest(_notification.Get());

        await _usuarioService.CadastrarDinamicoAsync(usuario);
        return Ok(new { success = true, message = "Usuario cadastrado com sucesso!", redirectUrl = Url.Action(nameof(Index), "Usuario") });
    }
}
