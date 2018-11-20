
namespace HallRental.Web.Services
{
    using HallRental.Web.Infrastructure;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System.Threading.Tasks;


    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(GlobalConstants.MyOperatingEmailName, GlobalConstants.MyOperatingEmail));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageBody.ToString()
            };

            using (var client = new SmtpClient())
            {
                client.Connect(GlobalConstants.EmailProviderOperatingEmail, GlobalConstants.PortNumberOperatingEmail);
                client.Authenticate(GlobalConstants.MyOperatingEmail, GlobalConstants.MyOperatingEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
