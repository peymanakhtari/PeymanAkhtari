using System.Net;
using System.Net.Mail;

namespace PeymanAkhtari.Utilities
{
    public class Email
    {
        public static Task SendEmailAsync(string toEmail, string subject, string message, bool isMessageHtml = false)
        {
            //MailMessage mail = new MailMessage();

            //mail.To.Add("peyman.akhtari.73@gmail.com");
            //mail.From = new MailAddress("peyman@Radicaltherapy.ir");
            //mail.Subject = "رادیکال تراپی_" + subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;

            //SmtpClient client = new SmtpClient("mail.Radicaltherapy.ir", 25);

            //client.Credentials = new System.Net.NetworkCredential("peyman@Radicaltherapy.ir", "VwGs2807lu");
            //client.Send(mail);


            using (var client = new SmtpClient("mail.peymanakhtari.ir",25))
            {

                var credentials = new NetworkCredential()
                {
                    UserName = "info@peymanakhtari.ir", // without @gmail.com
                    Password = "n32C1Mj6gu"
                };

                client.Credentials = credentials;

                using var emailMessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress("info@peymanakhtari.ir"), // with @gmail.com
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = isMessageHtml
                };

                client.Send(emailMessage);
            }

            return Task.CompletedTask;
        }
    }
}
