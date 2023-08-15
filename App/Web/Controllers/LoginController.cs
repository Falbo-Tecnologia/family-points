namespace Web.Controllers;

[Route("login")]
public class LoginController : Controller
{
    private readonly IEncryption _encryption;
    private readonly JwtSecretSetting _jwtSecretSetting;
    private readonly IUsuarioRepository _usuarioRepository;

    public LoginController(IEncryption encryption, AppSetting appSetting, IUsuarioRepository usuarioRepository)
    {
        _encryption = encryption;
        _jwtSecretSetting = appSetting.JwtSecret;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index() => View();

    [HttpGet("autenticar")]
    public async Task<IActionResult> Autenticar(string apelido, string senha)
    {
        var usuario = await _usuarioRepository.BuscarPorApelidoAsync(apelido);

        if (usuario == null)
            return NotFound("Não foi possível encontrar o usuário");

        if (!_encryption.Verify(usuario.Senha, _encryption.GenerateHash(_encryption.Encrypt(senha))))
            return BadRequest("Senha inválida");

        var jwtToken = TokenService.GenerateToken(usuario, _jwtSecretSetting.Key);

        Response.Cookies.Append(_jwtSecretSetting.CookieName, jwtToken);

        return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(_jwtSecretSetting.CookieName);

        return RedirectToAction(nameof(Index), "Login");
    }
}
