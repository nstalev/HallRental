
namespace HallRental.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Models;
    using HallRental.Services.Models.Events;

    public class CalendarService : ICalendarService
    {
        private readonly HallRentalDbContext db;

        public CalendarService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EventCalendarServiceModel> AllEvents()
        {
            return this.db.Events
                .Select(e => new EventCalendarServiceModel
                {
                    Title = e.EventTitle,
                    Start = e.EventDate,
                    Descrtiption = e.Description
                })
                .ToArray();
        }


    }
}
