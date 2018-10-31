
namespace HallRental.Services
{
    using HallRental.Data.Enums;
    using HallRental.Data.Models;
    using HallRental.Services.Models.Profile;
    using System;
    using static HallRental.Data.Enums.Enums;

    public interface IEventsService
    {

        void Create(int hallId,
                    string tenantId,
                    DateTime eventDate,
                    RentTimeEnum rentTime,
                    string fullName,
                    string email,
                    string phoneNumber,
                    DateTime eventStart,
                    DateTime eventEnd,
                    int numberOfPeople,
                    bool usingTablesAndChairs,
                    decimal tablesAndChairsCostPerPerson,
                    decimal securityGuardCostPerHour,
                    bool parkingLotSecurityService,
                    int parkingLotSecurityHours,
                    DateTime securityStartTime,
                    DateTime securityEndTime,
                    decimal hallRentalPrice,
                    decimal tablesAndChairsPrice,
                    decimal parkingLotSecurityPrice,
                    decimal securityDeposit,
                    decimal totalPrice,
                    string description,
                    string caterer,
                    string eventTitle);

        bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId);

        decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime);

        string GetRentTimeDisplay(RentTimeEnum rentTime);

        DateTime GetStartTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

        DateTime GetEndTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

        decimal CalculateSecurityDeposit(RentTimeEnum rentTime, DateTime eventEnd, DateTime date, decimal securityDepositBefore10pm, decimal securityDepositAfter10pm);

        bool CheckIfEventExists(int id);
        EventDetailsServiceModel EventById(int id);


        byte[] GeneratePdf(EventDetailsServiceModel currentEvent);
        string GetTextBodyForEmailForReservation(DateTime date,
                                                string fullName,
                                                string email,
                                                string phoneNumber,
                                                int numberOfPeople,
                                                decimal totalPrice);
    }

}
