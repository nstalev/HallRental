
namespace HallRental.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Admin.Models;

    public class IdentityService : IIdentityService
    {

        private readonly HallRentalDbContext db;

        public IdentityService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserModel> AllUsers()
        {
            return this.db.Users
                .Select(u => new UserModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    PhoneNumber = u.PhoneNumber
                }).ToList();
                
        }
    }
}
