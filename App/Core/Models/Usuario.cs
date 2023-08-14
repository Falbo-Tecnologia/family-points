namespace Core.Models;

public class Usuario
{
    public int Id { get; set; }
    public int IdTipoUsuario { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Apelido { get; set; }
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public int UsuarioCadastro { get; set; }

    public IEnumerable<Tarefa> Tarefas { get; set; }
    public IEnumerable<UsuarioOpcao> UsuarioOpcoes { get; set; }
    public TipoUsuario TipoUsuario { get; set; }

    public bool IsValid(INotification notification)
    {
        if (string.IsNullOrEmpty(Nome))
            notification.Add("É obrigatório informar o nome");

        if (string.IsNullOrEmpty(Email))
            notification.Add("É obrigatório informar o e-mail");

        if (string.IsNullOrEmpty(Apelido))
            notification.Add("É obrigatório informar o apelido");

        if (string.IsNullOrEmpty(Senha))
            notification.Add("É obrigatório informar a senha");

        return !notification.Any();
    }
}
