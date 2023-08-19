namespace Core.Models;
public class TarefaUsuario
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public int IdTarefa { get; set; }
    public DateTime DataCadastro { get; set; }
    public int UsuarioCadastro { get; set; }

    public Usuario Usuario { get; set; }
    public Tarefa Tarefa { get; set; }
}
