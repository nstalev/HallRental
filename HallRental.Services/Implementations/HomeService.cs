
namespace HallRental.Services.Implementations
{
    using HallRental.Data;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Text;

    public class HomeService : IHomeService
    {
        private readonly HallRentalDbContext db;

        public HomeService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public void SendEmail(string name, string email, string subject, string messageBody)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, email));
            message.To.Add(new MailboxAddress(name, ServiceConstants.ContactFormEmail));
            message.Subject = subject;

            var sb = new StringBuilder();
            sb.Append($"email from: {name}");
            sb.Append(Environment.NewLine);
            sb.Append($"email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append(messageBody);


            message.Body = new TextPart("plain")
            {

                Text = sb.ToString()

            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                client.Authenticate(ServiceConstants.ContactFormEmail, ServiceConstants.ContactFormEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }


        }
    }
}
