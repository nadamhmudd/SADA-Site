using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace SADA.Infrastructure.ThirdPartyServices;
public class EmailSender : IEmailSender
{
    private readonly MailSetting _mailSettings;

    public EmailSender(IOptions<MailSetting> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailToSend = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Email),
            Subject = subject
        };
        emailToSend.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));
        emailToSend.To.Add(MailboxAddress.Parse(email));
        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

        //send email
        using (var emailClient = new SmtpClient())
        {
            emailClient.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            emailClient.Authenticate(_mailSettings.Email, _mailSettings.Password);
            await emailClient.SendAsync(emailToSend);

            emailClient.Disconnect(true);
        }
    }
}
