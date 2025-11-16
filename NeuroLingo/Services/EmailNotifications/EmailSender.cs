using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NeuroLingo.Services.EmailNotifications;

public sealed class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        string? apiKey = _configuration["SENDGRID_API_KEY"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("mikolajkocik02@gmail.com", "NeuroLingo");
        var to = new EmailAddress(email);
        SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent: null, htmlMessage);
        Response response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Body.ReadAsStringAsync();
            throw new InvalidOperationException($"SendGrid error: {response.StatusCode}");
        }
    }
}
