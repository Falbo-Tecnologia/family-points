namespace Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetUsuariosAsync();
    Task<Usuario> GetUsuarioAsync(int id);
    Task PutUsuarioAsync(Usuario usuario);
}
