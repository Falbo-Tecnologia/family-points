namespace Core.Extensions;

public static class DecodeToken
{
    public enum PropertyTokenEnum
    {
        Email = 1,
        Apelido = 2,
        opcaoMae = 3,
        opcaoSistema = 4,
        idOpcao = 5
    }

    public static JwtSecurityToken Handler(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var readToken = handler.ReadToken(token) as JwtSecurityToken;

        return readToken;
    }

    public static int GetId(string token)
    {
        var readToken = Handler(token);
        var idUsuario = readToken.Claims.ToList()[0].Value;

        if (int.TryParse(idUsuario, out int num))
            return num;

        return default;
    }

    public static string GetProperty(string token, PropertyTokenEnum propertyTokenEnum)
    {
        var readToken = Handler(token);

        return readToken.Claims.ToList()[(int)propertyTokenEnum].Value;
    }
}
