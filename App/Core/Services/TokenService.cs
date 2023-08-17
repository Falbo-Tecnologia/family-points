namespace Core.Services;

public class TokenService
{
    public static string GenerateToken(Usuario usuario, string chave)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(chave);

        var opcaoMae = string.Join(",", usuario.UsuarioOpcoes.Select(x => x.OpcaoSistema.IdOpcaoMae));
        var opcao = string.Join(",", usuario.UsuarioOpcoes.Select(x => x.OpcaoSistema.Descricao));
        var idOpcao = string.Join(",", usuario.UsuarioOpcoes.Select(x => x.OpcaoSistema.Id));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Apelido),
                new Claim(ClaimTypes.Name, opcaoMae),
                new Claim(ClaimTypes.Name, opcao),
                new Claim(ClaimTypes.Name, idOpcao)
            }),

            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public static JwtSecurityToken Handler(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadToken(jwtToken) as JwtSecurityToken;

        return token;
    }

    public static int IdUsuarioLogado(string jwtToken)
    {
        var token = Handler(jwtToken);

        int.TryParse(token.Claims.FirstOrDefault(x => x.Type == "idUsuario")?.Value, out int idUsuarioLogado);

        return idUsuarioLogado;
    }

    public static string BuscaOpcoesUsuario(string jwtToken, int claim)
    {
        var token = Handler(jwtToken);

        return token.Claims.ToList()[claim].Value;
    }
}
