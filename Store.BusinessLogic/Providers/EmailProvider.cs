using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Providers
{
    public class EmailProvider
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(EmailOptions.NAME, EmailOptions.ADDRESS));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(EmailOptions.GMAIL_CONNECTION);
                await client.AuthenticateAsync(EmailOptions.ADDRESS, EmailOptions.PASSWORD);
                await client.SendAsync(emailMessage);
            }
        }
    }
}