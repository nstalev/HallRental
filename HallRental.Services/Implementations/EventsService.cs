
namespace HallRental.Services.Implementations
{
    using HallRental.Data;
    using HallRental.Data.Models;
    using System;

    public class EventsService : IEventsService
    {
        private readonly HallRentalDbContext db;

        public EventsService(HallRentalDbContext db)
        {
            this.db = db;
        }


        public void Create(string email, string phoneNumber, string description, string eventTitle, DateTime eventStart, DateTime eventEdn, int numberOfPeople)
        {
            var newEvent = new Event()
            {
                Email = email,
                Description = description,
                EventEdn = eventEdn,
                EventStart = eventStart,
                PhoneNumber = phoneNumber,
                NumberOfPeople = numberOfPeople,
                IsConfirmed = false

            };

            this.db.Events.Add(newEvent);
            this.db.SaveChanges();
        }
    }
}
