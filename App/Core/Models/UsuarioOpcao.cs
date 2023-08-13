namespace Core.Models;
public class UsuarioOpcao
{
    public int IdUsuario { get; set; }
    public int IdOpcaoSistema { get; set; }
    public DateTime DataCadastro { get; set; }
    public int UsuarioCadastro { get; set; }

    public Usuario Usuario { get; set; }
    public OpcaoSistema OpcaoSistema { get; set; }
}
