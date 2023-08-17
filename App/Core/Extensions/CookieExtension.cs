namespace Core.Extensions;

public static class CookieExtension
{
    public static Usuario SerializarToken(this string cookie)
    {
        var idUsuario = DecodeToken.GetId(cookie);

        var opcaoMae = DecodeToken.GetProperty(cookie, DecodeToken.PropertyTokenEnum.opcaoMae)?.Split(",") ?? new string[0];
        var opcaoSistema = DecodeToken.GetProperty(cookie, DecodeToken.PropertyTokenEnum.opcaoSistema).Split(',');
        var idOpcaoSistema = DecodeToken.GetProperty(cookie, DecodeToken.PropertyTokenEnum.idOpcao).Split(',');

        IEnumerable<UsuarioOpcao> usuarioOpcoes = idOpcaoSistema.Select((idOpcaoStr, i) =>
        {
            int.TryParse(idOpcaoStr, out int idOpcao);

            int? idOpcaoMae = opcaoMae.Length > i && int.TryParse(opcaoMae[i], out int idOpcaoMaeValue) ? idOpcaoMaeValue : null;

            return new UsuarioOpcao
            {
                IdUsuario = idUsuario,
                IdOpcaoSistema = idOpcao,
                OpcaoSistema = new OpcaoSistema
                {
                    Id = idOpcao,
                    IdOpcaoMae = idOpcaoMae,
                    Descricao = opcaoSistema[i]
                }
            };
        });

        var usuario = new Usuario
        {
            Id = idUsuario,
            Apelido = DecodeToken.GetProperty(cookie, DecodeToken.PropertyTokenEnum.Apelido),
            Email = DecodeToken.GetProperty(cookie, DecodeToken.PropertyTokenEnum.Email),
            UsuarioOpcoes = usuarioOpcoes
        };

        return usuario;
    }
}
