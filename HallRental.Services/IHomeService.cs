
namespace HallRental.Services
{
    public interface IHomeService
    {
        string GetEmailTextBodyFromContactForm(string email, string name, string message);
    }
}
