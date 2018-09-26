using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using HallRental.Data;
using HallRental.Services.Admin.Models.Events;

namespace HallRental.Services.Admin.Implementations
{
    public class EventsAdminService : IEventsAdminService
    {

        private readonly HallRentalDbContext db;

        public EventsAdminService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EventsListServiceModel> GetEventRequests(string search, int page, int pageSize)
        {
            return this.db.Events
                    .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                    && e.IsReservationConfirmed == false)
                    .OrderBy(e => e.EventDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<EventsListServiceModel>()
                    .ToList();
        }

        public int TotalEvetRequests(string search)
        {
            return this.db.Events
                    .Where(e => e.FullName.ToLower().Contains(search.ToLower()))
                    .Count();
        }   
    }
}
