using System.Net;
using System.Net.Mail;

namespace Demo.presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("khaledsaied080@gmail.com", "cnjcwdxegglfoucp");

            Client.Send("khaledsaied080@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
