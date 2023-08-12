namespace Core.Settings;

public class EncryptionSetting
{
    public int WorkFactor { get; set; }
    public string Prefix { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public int IterationCount { get; set; }
}
