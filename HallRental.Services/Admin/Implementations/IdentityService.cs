
namespace HallRental.Services.Admin.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Services.Admin.Models;

    public class IdentityService : IIdentityService
    {

        private readonly HallRentalDbContext db;

        public IdentityService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<UserModel> GetUsers(string search, int page, int pageSize)
        {
            return this.db.Users
                .Where(e => (e.UserName.ToLower().Contains(search.ToLower())
                || e.FirstName.ToLower().Contains(search.ToLower())
                || e.LastName.ToLower().Contains(search.ToLower())))
                .OrderBy(a => a.UserName)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<UserModel>()
                .ToList();

        }


        public int AllUsersCount(string search)
        {
            return this.db.Users
                .Where(e => (e.UserName.ToLower().Contains(search.ToLower())
                || e.FirstName.ToLower().Contains(search.ToLower())
                || e.LastName.ToLower().Contains(search.ToLower())))
                .Count();
        }

        public void DeleteEvents(string id)
        {
            var eventsForDelete = this.db.Events
                .Where(e => e.TenantId == id)
                .ToList();

            this.db.Events.RemoveRange(eventsForDelete);
            this.db.SaveChanges();
        }

        
    }
}
