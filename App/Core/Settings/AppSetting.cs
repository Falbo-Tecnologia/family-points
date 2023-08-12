namespace Core.Settings;

public class AppSetting
{
    public string PathLog { get; set; }
    public string EnvironmentName { get; set; }
    public bool HttpsRedirection { get; set; }
    public DatabaseSetting Database { get; set; }
    public EmailSetting Email { get; set; }
    public EncryptionSetting Encryption { get; set; }

    public static string GetApplicationName() => Assembly.GetEntryAssembly().GetName().Name;
}
