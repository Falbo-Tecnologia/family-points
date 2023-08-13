namespace Core.Models;
public class OpcaoSistema
{
    public int Id { get; set; }
    public int IdOpcaoMae { get; set; }
    public string Descricao { get; set; }

    public Usuario Usuario { get; set; }
    
    public OpcaoSistema OpcaoMae { get; set; }
    public IEnumerable<OpcaoSistema> OpcoesFilhas { get; set; }
    public IEnumerable<UsuarioOpcao> UsuarioOpcoes { get; set; }
}
