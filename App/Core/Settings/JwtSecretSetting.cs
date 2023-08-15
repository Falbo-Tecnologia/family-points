namespace Core.Settings;

public class JwtSecretSetting
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int Expiration { get; set; }
    public string CookieName { get; set; }
}
