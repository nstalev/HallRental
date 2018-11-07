
namespace HallRental.Services.Admin.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Enums;
    using HallRental.Data.Models;
    using HallRental.Services.Admin.Models.Events;


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
                    .Where(e => (e.FullName.ToLower().Contains(search.ToLower())
                    || e.Id.ToString() == search)
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
                    .Where(e => (e.FullName.ToLower().Contains(search.ToLower())
                    || e.Id.ToString() == search)
                    && e.IsReservationConfirmed == true
                    && e.EventDate.Date >= currentDate.Date)
                    .OrderBy(e => e.EventDate)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<EventsListServiceModel>()
                    .ToList();
        }

        public IEnumerable<EventsListServiceModel> GetPassedEvents(string search, int page, int pageSize, DateTime currentDate)
        {
            return this.db.Events
                   .Where(e => (e.FullName.ToLower().Contains(search.ToLower())
                    || e.Id.ToString() == search)
                   && e.IsReservationConfirmed == true
                   && e.EventDate.Date < currentDate.Date)
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

        public EventDetailsAdminSM EventById(int id)
        {
            return this.db.Events
              .Where(e => e.Id == id)
              .ProjectTo<EventDetailsAdminSM>()
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

        public void DisConfirmEvent(int id)
        {
            var currentEvent = this.db.Events.Find(id);

            currentEvent.IsReservationConfirmed = false;
            this.db.SaveChanges();
        }

        public List<EvenAlertNotificationSM> GetAllEventsOnTheSameDay(int id, DateTime checkDate)
        {
            return  this.db.Events
                .Where(e => e.EventDate.Date == checkDate.Date
                && e.Id != id)
                .ProjectTo<EvenAlertNotificationSM>()
                .ToList();
        }

        public void DeleteEvent(int id)
        {
            Event currentEvent = this.db.Events.Find(id);

            this.db.Events.Remove(currentEvent);
            this.db.SaveChanges();
        }

        public int AllEventRequestsCount()
        {
            return this.db.Events
                .Where(e => e.IsReservationConfirmed == false)
                .Count();
        }

        public EditEventServiceModel GetEventByIdForEdit(int id)
        {
            return this.db.Events
              .Where(e => e.Id == id)
              .ProjectTo<EditEventServiceModel>()
              .FirstOrDefault();
        }

        public void EditEvent(int id,
            string email,
            string phoneNumber,
            string fullName,
            int hallId,
            DateTime eventDate,
            Enums.RentTimeEnum rentTime,
            DateTime eventStart,
            DateTime eventEnd,
            int numberOfPeople,
            string typeOfEvent,
            string description,
            string caterer,
            bool usingTablesAndChairs,
            decimal tablesAndChairsCostPerPerson,
            bool parkingLotSecurityService,
            int parkingLotSecurityHours,
            DateTime securityStartTime,
            DateTime securityEndTime,
            decimal securityCostPerHour,
            decimal additionalCharges,
            string additionalChargesInformation,
            decimal hallRentalPrice,
            decimal tablesAndChairsPrice,
            decimal parkingLotSecurityPrice,
            decimal securityDeposit,
            decimal totalPrice)
        {

            Event currentEvent = this.db.Events.Find(id);

            currentEvent.Email = email;
            currentEvent.PhoneNumber = phoneNumber;
            currentEvent.FullName = fullName;
            currentEvent.HallId = hallId;
            currentEvent.EventDate = eventDate;
            currentEvent.RentTime = rentTime;
            currentEvent.EventStart = eventStart;
            currentEvent.EventEnd = eventEnd;
            currentEvent.NumberOfPeople = numberOfPeople;
            currentEvent.TypeOfEvent = typeOfEvent;
            currentEvent.Description = description;
            currentEvent.Caterer = caterer;
            currentEvent.UsingTablesAndChairs = usingTablesAndChairs;
            currentEvent.TablesAndChairsCostPerPerson = tablesAndChairsCostPerPerson;
            currentEvent.ParkingLotSecurityService = parkingLotSecurityService;
            currentEvent.ParkingLotSecurityHours = parkingLotSecurityHours;
            currentEvent.SecurityStartTime = securityStartTime;
            currentEvent.SecurityEndTime = securityEndTime;
            currentEvent.SecurityCostPerHour = securityCostPerHour;
            currentEvent.AdditionalCharges = additionalCharges;
            currentEvent.AdditionalChargesInformation = additionalChargesInformation;
            currentEvent.HallRentalPrice = hallRentalPrice;
            currentEvent.TablesAndChairsPrice = tablesAndChairsPrice;
            currentEvent.ParkingLotSecurityPrice = parkingLotSecurityPrice;
            currentEvent.SecurityDeposit = securityDeposit;
            currentEvent.TotalPrice = totalPrice;

            this.db.SaveChanges();
        }

        //GENERATE EMAIL CONFIRMATION SERVICE 
        public string GetEmailConfirmationTextBody(EventDetailsAdminSM currentEvent)
        {
            var sb = new StringBuilder();
            sb.Append($"Dear, {currentEvent.FullName}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("We would like to inform you that your reservation has been confirmed.");
            sb.Append(Environment.NewLine);
            sb.Append("Below you can find detailed information about the event and price calculation.");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Event number: {currentEvent.Id}");
            sb.Append(Environment.NewLine);
            sb.Append($"Hall: {currentEvent.HallName}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event Date: {currentEvent.EventDate}");
            sb.Append(Environment.NewLine);
            sb.Append($"Rent Time: {currentEvent.RentTimeDisplay}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event Start Time: {currentEvent.EventStart}");
            sb.Append(Environment.NewLine);
            sb.Append($"Event End Time: {currentEvent.EventEnd}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Price Information:");
            sb.Append(Environment.NewLine);
            sb.Append($"Hall Rental price: ${currentEvent.HallRentalPrice.ToString("F")}");
            sb.Append(Environment.NewLine);
            if (currentEvent.UsingTablesAndChairs)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Using tables and chairs price: ${currentEvent.TablesAndChairsPrice.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Tables and chairs cost per person: ${currentEvent.TablesAndChairsCostPerPerson.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Number of people: {currentEvent.NumberOfPeople}");
                sb.Append(Environment.NewLine);
            }
            if (currentEvent.ParkingLotSecurityService)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Parking Lot Security Price: ${currentEvent.ParkingLotSecurityPrice.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Parking Lot Security Hours: {currentEvent.ParkingLotSecurityHours}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security service cost per hour: ${currentEvent.SecurityCostPerHour.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security Start Time: {currentEvent.SecurityStartTime}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Security End Time: {currentEvent.SecurityEndTime}");
                sb.Append(Environment.NewLine);
            }
            if (currentEvent.AdditionalCharges > 0)
            {
                sb.Append(Environment.NewLine);
                sb.Append($"Additional Charges: ${currentEvent.AdditionalCharges.ToString("F")}");
                sb.Append(Environment.NewLine);
                sb.Append($"---Additional Charges Info: {currentEvent.SecurityEndTime}");
                sb.Append(Environment.NewLine);


            }
            sb.Append(Environment.NewLine);
            sb.Append($"Security Deposit: ${currentEvent.SecurityDeposit.ToString("F")}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append($"Total Price: ${currentEvent.TotalPrice.ToString("F")}");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            sb.Append("This is an automatically generated email – please do not reply to it.");
            sb.Append(Environment.NewLine);
            sb.Append("If you have any queries regarding your resevation please use our contact form or email to ...");

            return sb.ToString();
        }

       

       
    }
}
