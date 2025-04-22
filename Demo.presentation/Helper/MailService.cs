//using System.Net.Mail;
using Demo.presentation.Settings;
using Demo.presentation.Utilities;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Demo.presentation.Helper
{
    public class MailService(IOptions<MailSettings> _options) : IMailService
    {
        public void Send(Email email)
        {
            //1=> 
            var mail = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.Subject
            };
            //2=>
            mail.To.Add(MailboxAddress.Parse(email.To));

            //3=>
            mail.From.Add(new MailboxAddress(_options.Value.Email, _options.Value.DisplayName));


            //4=>
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            //5=>
            using var smtp = new SmtpClient();
            smtp.Connect(_options.Value.Host, _options.Value.Port, MailKit.Security.SecureSocketOptions.StartTls);

            smtp.Authenticate(_options.Value.Email, _options.Value.Password);

            smtp.Send(mail);

            smtp.Disconnect(true);

        }
    }
}
