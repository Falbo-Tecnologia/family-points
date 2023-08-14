namespace Core.Interfaces.Services;

public interface IUsuarioService
{
    Task CadastrarDinamicoAsync(Usuario usuario = null, IEnumerable<Usuario> usuarios = null);
}
