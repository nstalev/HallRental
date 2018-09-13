
namespace HallRental.Services.Implementations
{
    using HallRental.Data;
    using HallRental.Data.Enums;
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


        public void Create(int hallId, DateTime eventDate, RentTimeEnum rentTime, string fullName, string email, string phoneNumber, string eventStart, string eventEnd, int numberOfPeople, bool usingTablesAndChairs, decimal securityGuardCostPerHour, int securityGuards, double requestedSecurityHoursPerGuard, decimal hallRentalPrice, decimal tablesAndChairsPrice, decimal securityPrice, decimal totalPrice, string description, string eventTitle)
        {
            throw new NotImplementedException();
        }




        public bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId)
        {

            if (rentTime == RentTimeEnum.AllDay)
            {
                return this.db.Events
                    .Any(e => e.EventDate.Date == date.Date
                    && e.HallId == hallId);
            }
            else
            {
                var AllDayEvent = this.db.Events
                 .Any(e => e.EventDate.Date == date.Date
                 && e.RentTime == RentTimeEnum.AllDay
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
                  && e.RentTime != RentTimeEnum.AllDay);
                }

            }
          
        }

        public string GetRentTimeDisplay(RentTimeEnum rentTime)
        {
            if (rentTime == RentTimeEnum.EightAMtoThreePM)
            {
                return "8:00 am to 3:00 pm";
            }
            else if (rentTime == RentTimeEnum.FourPMtoMidNight)
            {
                return "4:00 pm to Midnight";
            }
            else
            {
                return "All day";
            }
        }


        public decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime)
        {
            if (rentTime == RentTimeEnum.EightAMtoThreePM)
            {
                if (eventDate == DayOfWeek.Monday
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday
                   || eventDate == DayOfWeek.Friday)
                {
                    return currentHall.MondayFriday8amTo3pm;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday8amTo3pm;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday8amTo3pm;
                }
            }
            else if (rentTime == RentTimeEnum.FourPMtoMidNight)
            {
                if (eventDate == DayOfWeek.Monday
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday)
                {
                    return currentHall.MondayThursday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Friday)
                {
                    return currentHall.Friday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday4pmToMN;
                }
            }
            else if (rentTime == RentTimeEnum.AllDay)
            {
                if (eventDate == DayOfWeek.Monday
                   || eventDate == DayOfWeek.Tuesday
                   || eventDate == DayOfWeek.Wednesday
                   || eventDate == DayOfWeek.Thursday)
                {
                    return currentHall.MondayFriday8amTo3pm + currentHall.MondayThursday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Friday)
                {
                    return currentHall.MondayFriday8amTo3pm + currentHall.Friday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Saturday)
                {
                    return currentHall.Saturday8amTo3pm + currentHall.Saturday4pmToMN;
                }
                else if (eventDate == DayOfWeek.Sunday)
                {
                    return currentHall.Sunday8amTo3pm + currentHall.Sunday4pmToMN;
                }
            }

            return 0;
        }

        public DateTime GetStartTimeDefault(RentTimeEnum rentTime, DateTime eventDate)
        {
            DateTime result = eventDate;

            if (rentTime == RentTimeEnum.EightAMtoThreePM || rentTime == RentTimeEnum.AllDay)
            {
                result = result.Add(new TimeSpan(8, 00, 0));
            }
            if (rentTime == RentTimeEnum.FourPMtoMidNight)
            {
                result = result.Add(new TimeSpan(16, 00, 0));
            }

            return result;
        }

        public DateTime GetEndTimeDefault(RentTimeEnum rentTime, DateTime eventDate)
        {
            DateTime result = eventDate;

            if (rentTime == RentTimeEnum.EightAMtoThreePM)
            {
                result = result.Add(new TimeSpan(15, 00, 0));
            }
            if (rentTime == RentTimeEnum.FourPMtoMidNight || rentTime == RentTimeEnum.AllDay)
            {
                result = result.Add(new TimeSpan(23, 50, 0));
            }

            return result;
        }

      
    }
}
