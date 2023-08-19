namespace Core.Models;
public class Tarefa
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public int Pontuacao { get; set; }
    public DateTime DataCadastro { get; set; }
    public int UsuarioCadastro { get; set; }

    public IEnumerable<TarefaUsuario> TarefasUsuarios { get; set; }
}
