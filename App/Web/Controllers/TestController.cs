namespace Web.Controllers
{
    public class TestController : AuthenticatedController
    {
        protected IEncryption Encryption { get; private set; }
        public TestController(IEncryption encryption)
        {
            Encryption = encryption;
        }
        
        public IActionResult Index() => VerificaSenha("123456");
        public IActionResult VerificaSenha(string senha)
        {
            var hash = Encryption.GenerateHash(senha);
            var verify = Encryption.Verify(senha, hash);
            var encrypt = Encryption.Encrypt(senha);
            var decrypt = Encryption.Decrypt(encrypt);

            if (!decrypt.Equals(senha))
                return BadRequest("Teste de criptografia falhou.");

            return Ok(new { senha, hash, verify, encrypt, decrypt });
        }
    }
}
