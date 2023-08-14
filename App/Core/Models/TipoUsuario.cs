namespace Core.Models;

public class TipoUsuario
{
    public int Id { get; set; }
    public string Tipo { get; set; }

    public IEnumerable<Usuario> Usuarios { get; set; }

    public enum TipoEnum : int
    {
        Responsavel = 1,
        Crianca = 2
    }

    public static Dictionary<int, string> tipo = new()
    {
        { 1, "Responsável" },
        { 2, "Criança" }
    };
}
