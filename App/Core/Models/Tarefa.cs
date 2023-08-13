namespace Core.Models;
public class Tarefa
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string Descricao { get; set; }
    public int Pontuacao { get; set; }
    public DateTime DataCadastro { get; set; }
    public int UsuarioCadastro { get; set; }

    public Usuario Usuario { get; set; }
}
