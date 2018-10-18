
namespace HallRental.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Models;
    using HallRental.Services.Models.Profile;

    public class ProfileService : IProfileService
    {
        private readonly HallRentalDbContext db;

        public ProfileService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<MyEventsServiceModel> MyFutureEvents(string userId, int page, int pageSize, DateTime markDate)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId
                    && e.EventDate.Date >= markDate.Date)
                    .OrderBy(e => e.EventDate)
                    .Skip((page-1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<MyEventsServiceModel>()
                    .ToList();
        }

        public int TotalFutureEvents(string userId, DateTime markDate)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId
                    && e.EventDate.Date >= markDate.Date)
                    .Count();
        }

        public IEnumerable<MyEventsServiceModel> MyPassedEvents(string userId, int page, int pageSize, DateTime markDate)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId
                    && e.EventDate.Date < markDate.Date)
                    .OrderByDescending(e => e.EventDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<MyEventsServiceModel>()
                    .ToList();
        }

        public int TotalPassedEvents(string userId, DateTime markDate)
        {
            return this.db.Events
                    .Where(e => e.TenantId == userId
                    && e.EventDate.Date < markDate.Date)
                    .Count();
        }

        //public EventDetailsServiceModel EventById(int id)
        //{
        //    return this.db.Events
        //        .Where(e => e.Id == id)
        //        .ProjectTo<EventDetailsServiceModel>()
        //        .FirstOrDefault();
                
        //}

      
    }
}
