using System;

namespace HallRental.Services
{
    public interface IEventsService
    {

      

        void Create(string email,
                    string phoneNumber,
                    string description,
                    string eventTitle,
                    DateTime eventStart,
                    DateTime eventEdn,
                    int numberOfPeople);
    }
}
