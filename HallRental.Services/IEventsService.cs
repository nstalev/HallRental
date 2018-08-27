
namespace HallRental.Services
{
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
                    decimal totalPrice);

        bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId);

    }
}
