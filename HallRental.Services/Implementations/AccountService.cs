
namespace HallRental.Services.Implementations
{
    using HallRental.Data;
    using System.Linq;

    public class AccountService: IAccountService
    {
        private readonly HallRentalDbContext db;

        public AccountService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public bool CheckIfEmailExists(string email)
        {
            return this.db.Users.Any(u => u.Email == email);
        }
    }
}
