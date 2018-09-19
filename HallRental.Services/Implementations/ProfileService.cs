
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

        public IEnumerable<MyEventsServiceModel> MyEvents(string userId, int page, int pageSize)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId)
                    .OrderBy(e => e.EventDate)
                    .Skip((page-1) * pageSize)
                    .Take(pageSize)
                    .Select(e => new MyEventsServiceModel
                    {
                        HallId = e.HallId,
                        Date = e.EventDate,
                        IsReservationConfirmed = e.IsReservationConfirmed,
                        NumberOfPeople = e.NumberOfPeople,
                        RentTime = e.RentTime,
                        Totalprice = e.TotalPrice,
                        HallName = e.Hall.Name,
                        EventTitle = e.EventTitle
                    })
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
