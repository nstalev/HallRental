using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using HallRental.Data;
using HallRental.Services.Admin.Models.Events;
using HallRental.Services.Models.Profile;

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

        public IEnumerable<EventsListServiceModel> GetConfirmedUpcomingEvents(string search, int page, int pageSize, DateTime currentDate)
        {
            return this.db.Events
                    .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                    && e.IsReservationConfirmed == true
                    && e.EventDate >= currentDate)
                    .OrderBy(e => e.EventDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<EventsListServiceModel>()
                    .ToList();
        }

        public IEnumerable<EventsListServiceModel> GetPassedEvents(string search, int page, int pageSize, DateTime currentDate)
        {
            return this.db.Events
                   .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                   && e.IsReservationConfirmed == true
                   && e.EventDate < currentDate)
                   .OrderByDescending(e => e.EventDate)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .ProjectTo<EventsListServiceModel>()
                   .ToList();
        }

       


        public int TotalEvetRequests(string search)
        {
            return this.db.Events
                    .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                    && e.IsReservationConfirmed == false)
                    .Count();
        }

        public int TotalConfirmedUpcomingEvents(string search, DateTime currentDate)
        {
            return this.db.Events
                    .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                    && e.EventDate >= currentDate
                    && e.IsReservationConfirmed == true)
                    .Count();
        }

        public int TotalPassedEvents(string search, DateTime currentDate)
        {
            return this.db.Events
                   .Where(e => e.FullName.ToLower().Contains(search.ToLower())
                   && e.EventDate < currentDate
                   && e.IsReservationConfirmed == true)
                   .Count();
        }

        public EventDetailsServiceModel EventById(int id)
        {
            return this.db.Events
              .Where(e => e.Id == id)
              .ProjectTo<EventDetailsServiceModel>()
              .FirstOrDefault();
        }

        public bool EventExists(int id)
        {
            return this.db.Events.Any(e => e.Id == id);
        }

        public void ConfirmEvent(int id)
        {
            var currentEvent = this.db.Events.Find(id);

            currentEvent.IsReservationConfirmed = true;
            this.db.SaveChanges();
        }

       
    }
}
