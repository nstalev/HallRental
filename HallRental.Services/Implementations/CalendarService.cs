
namespace HallRental.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using HallRental.Data;
    using HallRental.Services.Models;

    public class CalendarService : ICalendarService
    {
        private readonly HallRentalDbContext db;

        public CalendarService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<EventServiceModel> AllEvents()
        {
            return this.db.Events
                .Select(e => new EventServiceModel
                {
                    Title = e.EventTitle,
                    Start = e.EventStart,
                    End = e.EventEdn,
                    Descrtiption = e.Description
                })
                .ToArray();
        }
    }
}
