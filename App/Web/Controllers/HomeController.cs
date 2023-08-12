namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly IEncryption _encryption;

    public HomeController(IEncryption encryption)
    {
        _encryption = encryption;
    }
    public IActionResult Index()
    {
        var senha = "123456";
        var hash = _encryption.GenerateHash(senha);
        var verify = _encryption.Verify(senha, hash);
        var encrypt = _encryption.Encrypt(senha);
        var decrypt = _encryption.Decrypt(encrypt);

        if (!decrypt.Equals(senha))
            return BadRequest("Teste de criptografia falhou.");
            
        return Ok(new { senha, hash, verify, encrypt, decrypt });
    }

    public IActionResult Privacy() => View();

}
