using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Store.Shared.Options;
using System.Threading.Tasks;
using static Store.Shared.Constants.Constants;

namespace Store.BusinessLogic.Providers
{
    public class EmailProvider
    {
        private readonly IOptions<EmailOptions> _options;
        public EmailProvider(IOptions<EmailOptions> options)
        {
            _options = options;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.Value.Name, _options.Value.Address));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_options.Value.GmailConnection);
                await client.AuthenticateAsync(_options.Value.Address, _options.Value.Password);
                await client.SendAsync(emailMessage);
            }
        }
    }
}