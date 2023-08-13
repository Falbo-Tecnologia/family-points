namespace Core.Helpers.Email;

public class EmailOptions : IEmailOptions
{
    public string Subject { get; set; }
    public string Body { get; set; }
    public MailPriority Priority { get; set; }
    public bool IsBodyHtml { get; set; }
    public Encoding Encoding { get; set; }
}
