
namespace HallRental.Services
{
    using HallRental.Data.Models;
    using System;
    using static HallRental.Data.Enums.Enums;

    public interface IEventsService
    {

        void Create(int hallId,
                    DateTime eventDate,
                    RentTimeEnum rentTime,
                    string fullName,
                    string email,
                    string phoneNumber,
                    string eventStart,
                    string eventEnd,
                    int numberOfPeople,
                    bool usingTablesAndChairs,
                    decimal securityGuardCostPerHour,
                    int securityGuards,
                    double requestedSecurityHoursPerGuard,
                    decimal hallRentalPrice,
                    decimal tablesAndChairsPrice,
                    decimal securityPrice,
                    decimal totalPrice,
                    string description,
                    string eventTitle);

        bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId);

        decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime);

        string GetRentTimeDisplay(RentTimeEnum rentTime);

        DateTime GetStartTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

        DateTime GetEndTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

    }

}
