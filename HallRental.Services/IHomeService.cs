
namespace HallRental.Services
{
    public interface IHomeService
    {
        void SendEmail(string name, string email, string subject, string message);
    }
}
