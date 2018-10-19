
namespace HallRental.Services.Implementations
{
    using HallRental.Data;

    public class ManageService : IManageService
    {
        private readonly HallRentalDbContext db;

        public ManageService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public void UpdateUserName(string id, string firstName, string lastName)
        {
            var user = this.db.Users.Find(id);

            user.FirstName = firstName;
            user.LastName = lastName;

            this.db.SaveChanges();
        }
    }
}
