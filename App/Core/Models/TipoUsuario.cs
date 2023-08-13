namespace Core.Models;

public class TipoUsuario
{
    public int Id { get; set; }
    public string Tipo { get; set; }

    public IEnumerable<Usuario> Usuarios { get; set; }
}
