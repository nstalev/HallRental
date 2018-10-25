
namespace HallRental.Services.Implementations
{
    using System;
    using System.Text;

    public class HomeService : IHomeService
    {

        public string GetEmailTextBodyFromContactForm(string email, string name, string message)
        {
            var sb = new StringBuilder();
            sb.Append($"Email from: {name}");
            sb.Append(Environment.NewLine);
            sb.Append($"Email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(message);

            return sb.ToString();
        }

    }
}
