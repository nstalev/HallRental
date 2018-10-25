
namespace HallRental.Services.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Enums;
    using HallRental.Data.Models;
    using HallRental.Services.Models.Profile;
    using MailKit.Net.Smtp;
    using MimeKit;
    using System;
    using System.Linq;
    using System.Text;
    using static HallRental.Data.Enums.Enums;

    public class EventsService : IEventsService
    {
        private readonly HallRentalDbContext db;

        public EventsService(HallRentalDbContext db)
        {
            this.db = db;
        }


        public void Create(int hallId, string tenantId, DateTime eventDate, RentTimeEnum rentTime, string fullName, string email, string phoneNumber, DateTime eventStart, DateTime eventEnd, int numberOfPeople, bool usingTablesAndChairs, decimal tablesAndChairsCostPerPerson, decimal securityGuardCostPerHour, bool parkingLotSecurityService, int parkingLotSecurityHours, DateTime securityStartTime, DateTime securityEndTime, decimal hallRentalPrice, decimal tablesAndChairsPrice, decimal parkingLotSecurityPrice, decimal securityDeposit, decimal totalPrice, string description, string caterer, string eventTitle)
        {
            Event newEvent = new Event
            {
                 HallId = hallId,
                 TenantId = tenantId,
                 EventDate = rentTime == RentTimeEnum.EightAMtoThreePM ? eventDate + new TimeSpan(8, 00, 00) : eventDate + new TimeSpan(16, 00, 00),
                 RentTime = rentTime,
                 Email = email,
                 FullName = fullName,
                 PhoneNumber = phoneNumber,
                 EventStart = eventStart,
                 EventEnd = eventEnd,
                 NumberOfPeople = numberOfPeople,

                 UsingTablesAndChairs= usingTablesAndChairs,
                 TablesAndChairsCostPerPerson = tablesAndChairsCostPerPerson,
                 SecurityGuardCostPerHour = securityGuardCostPerHour,
                 ParkingLotSecurityService = parkingLotSecurityService,
                 ParkingLotSecurityHours = parkingLotSecurityHours,
                 SecurityStartTime = securityStartTime,
                 SecurityEndTime = securityEndTime,

                 EventTitle = eventTitle,
                 Description = description,
                 Caterer = caterer,

                 HallRentalPrice = hallRentalPrice,
                 TablesAndChairsPrice = tablesAndChairsPrice,
                 ParkingLotSecurityPrice = parkingLotSecurityPrice,
                 SecurityDeposit = securityDeposit,
                 TotalPrice = totalPrice,

                 IsReservationConfirmed = false

            };

            this.db.Events.Add(newEvent);
            this.db.SaveChanges();
        }




        public bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId)
        {

            if (rentTime == RentTimeEnum.AllDay)
            {
                return this.db.Events
                    .Any(e => e.EventDate.Date == date.Date
                    && e.HallId == hallId 
                    && e.IsReservationConfirmed == true);
            }
            else
            {
                var AllDayEvent = this.db.Events
                 .Any(e => e.EventDate.Date == date.Date
                 && e.RentTime == RentTimeEnum.AllDay
                  && e.HallId == hallId
                  && e.IsReservationConfirmed == true);

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
                  && e.RentTime != RentTimeEnum.AllDay
                  && e.IsReservationConfirmed ==true);
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
                result = result.Add(new TimeSpan(00, 00, 0)).AddDays(1);
            }

            return result;
        }


        public decimal CalculateSecurityDeposit(RentTimeEnum rentTime, decimal securityDepositBefore10pm, decimal securityDepositAfter10pm)
        {
            if (rentTime == RentTimeEnum.EightAMtoThreePM)
            {
                return securityDepositBefore10pm;
            }
            else
            {
                return securityDepositAfter10pm;
            }
        }

        public bool CheckIfEventExists(int id)
        {
            return this.db.Events.Any(e => e.Id == id);
        }


        public EventDetailsServiceModel EventById(int id)
        {
            return this.db.Events
               .Where(e => e.Id == id)
               .ProjectTo<EventDetailsServiceModel>()
               .FirstOrDefault();
        }

        public byte[] GeneratePdf(EventDetailsServiceModel currentEvent)
        {
            //ToDo

            throw new NotImplementedException();
        }


        //SEND EMAIL SERVICE FOR RESERVATIONS

        public void SendEmail(string name, string email, string subject, string messageBody)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(name, ServiceConstants.MyOperatingEmail));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = messageBody.ToString()
            };

            using (var client = new SmtpClient())
            {
                client.Connect(ServiceConstants.EmailProviderOperatingEmail, ServiceConstants.PortNumberOperatingEmail);
                client.Authenticate(ServiceConstants.MyOperatingEmail, ServiceConstants.MyOperatingEmailPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public string GetTextBodyForEmailForReservation(DateTime date,
                                                        string fullName,
                                                        string email,
                                                        string phoneNumber,
                                                        int numberOfPeople,
                                                        decimal totalPrice)
        {
            var sb = new StringBuilder();
            sb.Append($"Request for hall reservation on {date.ToShortDateString()}");
            sb.Append(Environment.NewLine);
            sb.Append($"Name: {fullName}");
            sb.Append(Environment.NewLine);
            sb.Append($"email: {email}");
            sb.Append(Environment.NewLine);
            sb.Append($"Phone Number: {phoneNumber}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Total Price: ${totalPrice.ToString("F")}");

            return sb.ToString();
        }
    }
}
