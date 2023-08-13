namespace Core.Interfaces.Helpers.Email;

public interface IEmailOptions
{
    string Subject { get; set; }
    string Body { get; set; }
    MailPriority Priority { get; set; }
    bool IsBodyHtml { get; set; }
    Encoding Encoding { get; set; }
}
