
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



        //SEND EMAIL SERVICE FROM CONTACT FORM

        public void SendEmail(string name, string email, string subject, string messageBody)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(ServiceConstants.ContactFormOperattingEmailName, ServiceConstants.MyOperatingEmail));
            message.To.Add(new MailboxAddress(ServiceConstants.ContactFormEmailToName, ServiceConstants.ContactFormEmailTo));
            message.Subject = subject;

            var sb = new StringBuilder();
            sb.Append($"email from: {name}");
            sb.Append(Environment.NewLine);
            sb.Append($"email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(messageBody);


            message.Body = new TextPart("plain")
            {

                Text = sb.ToString()

            };

            using (var client = new SmtpClient())
            {
                client.Connect(ServiceConstants.EmailProviderOperatingEmail, ServiceConstants.PortNumberOperatingEmail);
                client.Authenticate(ServiceConstants.MyOperatingEmail, ServiceConstants.MyOperatingEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }


        }
    }
}
