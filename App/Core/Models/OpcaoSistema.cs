namespace Core.Models;
public class OpcaoSistema
{
    public int Id { get; set; }
    public int? IdOpcaoMae { get; set; }
    public string Descricao { get; set; }

    public Usuario Usuario { get; set; }

    public OpcaoSistema OpcaoMae { get; set; }
    public IEnumerable<OpcaoSistema> OpcoesFilhas { get; set; }
    public IEnumerable<UsuarioOpcao> UsuarioOpcoes { get; set; }

    public enum OpcaoSistemaEnum : int
    {
        Seguranca = 1,
        Usuario = 2,
        Financeiro = 5,
        Pontos = 6,
        Relatorios = 7,
        Tarefa = 8,
    }

    public static Dictionary<int, string> tipo = new()
    {
        { 1, "Segurança" },
        { 2, "Usuário" }
    };
}
