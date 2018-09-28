
namespace HallRental.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Models;
    using HallRental.Services.Models.Events;
    using static HallRental.Data.Enums.Enums;

    public class CalendarService : ICalendarService
    {
        private readonly HallRentalDbContext db;

        public CalendarService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EventCalendarServiceModel> AllEvents(int hallId)
        {
            return this.db.Events
                .Where(e => e.HallId == hallId
                && e.IsReservationConfirmed == true)
                .Select(e => new EventCalendarServiceModel
                {
                    Title = e.EventTitle,
                    Start = e.EventDate,
                    Descrtiption = e.Description,
                    RentTime = e.RentTime
                })
                .ToArray();
        }


    }
}
