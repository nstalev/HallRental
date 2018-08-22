using System;
using System.Collections.Generic;
using HallRental.Services.Models.Events;
using static HallRental.Data.Enums.Enums;

namespace HallRental.Services
{
    public interface IEventsService
    {

      

        void Create(string email,
                    string phoneNumber,
                    string description,
                    string eventTitle,
                    DateTime eventDate,
                    RentTimeEnum timeRent,
                    int numberOfPeople);

        bool EventExists(DateTime date, RentTimeEnum rentTime);

    }
}
