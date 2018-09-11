
namespace HallRental.Services
{
    using HallRental.Data.Models;
    using System;
    using static HallRental.Data.Enums.Enums;

    public interface IEventsService
    {

        void Create(string email,
                    string phoneNumber,
                    string description,
                    string eventTitle,
                    DateTime eventDate,
                    RentTimeEnum timeRent,
                    int numberOfPeople,
                    decimal totalPrice,
                    int securityGuards,
                    bool withCHairsAndTable);

        bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId);

        decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime);

        string GetRentTimeDisplay(RentTimeEnum rentTime);

        DateTime GetStartTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

        DateTime GetEndTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

    }

}
