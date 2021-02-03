namespace IdentityProvider.Services
{
    // TODO : RM to add:
    //  1. Sendgrid nuget package
    //  2. Uncomment this codes below
    //  3. Add configuration to appsettings.json
    //  4. Add service to Startup.ConfigureServices()

    //services.AddTransient<IEmailSender, EmailSender>();

    // appsettings.json
    //"SendGrid": {
    //    "ApiKey": "xxxxxxxx",
    //    "FromEmail": "identity@server.com",
    //    "FromName": "Identity Server"
    //}

    //public class EmailSenderService : IEmailSender
    //{
    //    private readonly IConfiguration _configuration;

    //    public EmailSender(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public Task SendEmailAsync(string email, string subject, string message)
    //    {
    //        string key = _configuration.GetSection("SendGrid").GetValue<string>("ApiKey");
    //        return Execute(key, subject, message, email);
    //    }

    //    public Task Execute(string apiKey, string subject, string message, string email)
    //    {
    //        var client = new SendGridClient(apiKey);
    //        string fromEmail = _configuration.GetSection("SendGrid").GetValue<string>("FromEmail");
    //        string fromName = _configuration.GetSection("SendGrid").GetValue<string>("FromName");

    //        var msg = new SendGridMessage()
    //        {
    //            From = new EmailAddress(fromEmail, fromName),
    //            Subject = subject,
    //            PlainTextContent = message,
    //            HtmlContent = message
    //        };
    //        msg.AddTo(new EmailAddress(email));

    //        // Disable click tracking.
    //        // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
    //        msg.SetClickTracking(false, false);

    //        return client.SendEmailAsync(msg);
    //    }
    //}
}
