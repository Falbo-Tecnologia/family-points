namespace Core.Interfaces.Repositories;

public interface ITipoUsuarioRepository
{
    Task<IEnumerable<TipoUsuario>> ListarAsync();
    Task<TipoUsuario> BuscarAsync(int id);
}
