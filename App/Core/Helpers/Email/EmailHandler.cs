namespace Core.Helpers.Email;

public class EmailHandler : IEmailHandler
{
    private readonly EmailSetting _emailSetting;
    private readonly SmtpClient _smtpClient;

    public EmailHandler(AppSetting appSetting)
    {
        _emailSetting = appSetting.Email;
        _smtpClient = new SmtpClient(_emailSetting.Host, _emailSetting.Port)
        {
            Credentials = new NetworkCredential(_emailSetting.Username, _emailSetting.Password)
        };
    }

    public IEmailOptions Build(string subject, string body)
    {
        return Build(subject, body, MailPriority.Normal, true, null);
    }

    public IEmailOptions Build(string subject, string body, MailPriority priority, bool isBodyHtml, Encoding encoding)
    {
        return new EmailOptions
        {
            Subject = subject,
            Body = body,
            Priority = priority,
            IsBodyHtml = isBodyHtml,
            Encoding = encoding ?? Encoding.GetEncoding("ISO-8859-1")
        };
    }

    public async Task<IEmailOptions> BuildTemplate(string subject, string template, object replaces, MailPriority priority = MailPriority.Normal)
    {
        template = template.EndsWith(".html") ? template : template + ".html";
        var body = await File.ReadAllTextAsync($"EmailTemplates/" + template);

        foreach (var prop in replaces.GetType().GetProperties())
            body = body.Replace($"{{{prop.Name}}}", prop.GetValue(replaces)?.ToString());

        return Build(subject, body, priority, true, null);
    }

    public async Task SendAsync(string to, IEmailOptions options, IEnumerable<Attachment> attachments = null, IEnumerable<MailAddress> carbonCopies = null)
    {
        var msg = new MailMessage(_emailSetting.Username, to)
        {
            Subject = options.Subject,
            Body = options.Body,
            IsBodyHtml = options.IsBodyHtml,
            Priority = options.Priority,
            SubjectEncoding = options.Encoding,
            BodyEncoding = options.Encoding
        };

        if (attachments != null)
            foreach (var attachment in attachments)
                msg.Attachments.Add(attachment);

        if (carbonCopies != null)
            foreach (var carbonCopy in carbonCopies)
                msg.CC.Add(carbonCopy);

        await _smtpClient.SendMailAsync(msg);
    }

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}
