
namespace HallRental.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Services.Models.Profile;

    public class ProfileService : IProfileService
    {
        private readonly HallRentalDbContext db;

        public ProfileService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MyEventsServiceModel> MyEvents(string userId, int page, int pageSize)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId)
                    .OrderBy(e => e.EventDate)
                    .Skip((page-1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<MyEventsServiceModel>()
                    .ToList();
        }

        public int Total(string userId)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId)
                    .Count();
        }
    }
}
