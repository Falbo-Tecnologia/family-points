namespace Core.Interfaces.Helpers.Email;

public interface IEmailHandler
{
    IEmailOptions Build(string subject, string body);
    IEmailOptions Build(string subject, string body, MailPriority priority, bool isBodyHtml, Encoding encoding);
    Task<IEmailOptions> BuildTemplate(string subject, string template, object replaces, MailPriority priority = MailPriority.Normal);
    Task SendAsync(string to, IEmailOptions options, IEnumerable<Attachment> attachments = null, IEnumerable<MailAddress> carbonCopies = null);
}
