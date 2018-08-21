
namespace HallRental.Services.Implementations
{
    using HallRental.Data;
    using HallRental.Data.Models;
    using System;
    using System.Linq;
    using static HallRental.Data.Enums.Enums;

    public class EventsService : IEventsService
    {
        private readonly HallRentalDbContext db;

        public EventsService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public void Create(string email, string phoneNumber, string description, string eventTitle, DateTime evetnDate, RentTimeEnum rentTime, int numberOfPeople)
        {
            var newEvent = new Event()
            {
                Email = email,
                Description = description,
                EventDate = evetnDate,
                RentTime = rentTime,
                EventTitle = eventTitle,
                PhoneNumber = phoneNumber,
                NumberOfPeople = numberOfPeople,
                IsConfirmed = false

            };

            this.db.Events.Add(newEvent);
            this.db.SaveChanges();
        }



        public bool EventExists(DateTime date)
        {
            return this.db.Events.Any(e => e.EventDate == date);
        }
    }
}
