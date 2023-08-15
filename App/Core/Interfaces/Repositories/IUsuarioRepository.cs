namespace Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ListarAsync();
    Task<Usuario> BuscarAsync(int id);
    Task<Usuario> BuscarPorApelidoAsync(string apelido);
    Task CadastrarDinamicoAsync(Usuario usuario = null, IEnumerable<Usuario> usuarios = null);
}
