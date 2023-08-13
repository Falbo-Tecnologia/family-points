using Core.Interfaces.Repositories;

namespace Web.Controllers;

public class UsuarioController : AuthenticatedController
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IActionResult> Index() => View("Index", await _usuarioRepository.GetUsuariosAsync());
}
