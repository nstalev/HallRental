﻿
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

        public void Create(string email, string phoneNumber, string description, string eventTitle, DateTime evetnDate, RentTimeEnum rentTime, int numberOfPeople, decimal totalPrice)
        {
            var newEvent = new Event()
            {
                Email = email,
                Description = description,
                EventDate = rentTime == RentTimeEnum.eightAMtoThreePM ? evetnDate + new TimeSpan(8,00,00) : evetnDate + new TimeSpan(16, 00, 00),
                RentTime = rentTime,
                EventTitle = eventTitle,
                PhoneNumber = phoneNumber,
                NumberOfPeople = numberOfPeople,
                TotalPrice = totalPrice,
                IsConfirmed = false,
                HallId = 9

            };

            this.db.Events.Add(newEvent);
            this.db.SaveChanges();
        }



        public bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId)
        {

            if (rentTime == RentTimeEnum.allDay)
            {
                return this.db.Events
                    .Any(e => e.EventDate.Date == date.Date
                    && e.HallId == hallId);
            }
            else
            {
                var AllDayEvent = this.db.Events
                 .Any(e => e.EventDate.Date == date.Date
                 && e.RentTime == RentTimeEnum.allDay
                  && e.HallId == hallId);

                if (AllDayEvent)
                {
                    return true;
                }
                else
                {
                    return this.db.Events
                  .Any(e => e.EventDate.Date == date.Date
                   && e.HallId == hallId
                  && e.RentTime == rentTime
                  && e.RentTime != RentTimeEnum.allDay);
                }

            }
          
        }
    
    }
}