
namespace HallRental.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Models.Profile;

    public class ProfileService : IProfileService
    {
        private readonly HallRentalDbContext db;

        public ProfileService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MyEventsServiceModel> MyEvents(string userId)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId)
                    .OrderByDescending(e => e.EventDate)
                    .Select(e => new MyEventsServiceModel
                    {
                         HallId = e.HallId,
                         Date = e.EventDate,
                        IsReservationConfirmed = e.IsReservationConfirmed
                    })
                    .ToList();
        }
    }
}
