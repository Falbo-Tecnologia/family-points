namespace Core.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IEncryption _encryption;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITipoUsuarioRepository _tipoUsuarioRepository;

    public UsuarioService(IEncryption encryption, IUsuarioRepository usuarioRepository, ITipoUsuarioRepository tipoUsuarioRepository)
    {
        _encryption = encryption;
        _usuarioRepository = usuarioRepository;
        _tipoUsuarioRepository = tipoUsuarioRepository;
    }

    public async Task CadastrarDinamicoAsync(Usuario usuario = null, IEnumerable<Usuario> usuarios = null)
    {
        if (usuario != null)
            usuario = Tratar(usuario);

        if (usuarios != null)
            usuarios = TratarRange(usuarios);

        await _usuarioRepository.CadastrarDinamicoAsync(usuario, usuarios);
    }

    private Usuario Tratar(Usuario usuario)
    {
        var hash = _encryption.GenerateHash(usuario.Senha);

        if (_encryption.Verify(usuario.Senha, hash))
            usuario.Senha = _encryption.Encrypt(usuario.Senha);

        var novoUsuario = new Usuario
        {
            IdTipoUsuario = (int)Enum.Parse<TipoUsuario.TipoEnum>(usuario.TipoUsuario.Tipo),
            Nome = usuario.Nome,
            Email = usuario.Email,
            Apelido = usuario.Apelido,
            Senha = usuario.Senha,
            DataCadastro = DateTime.UtcNow,
            UsuarioCadastro = 1
        };

        return novoUsuario;
    }

    private IEnumerable<Usuario> TratarRange(IEnumerable<Usuario> usuarios)
    {
        var novosUsuarios = new List<Usuario>();

        foreach (var usuario in usuarios)
            novosUsuarios.Add(Tratar(usuario));

        return novosUsuarios;
    }
}
