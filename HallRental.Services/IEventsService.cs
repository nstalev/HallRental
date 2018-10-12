﻿
namespace HallRental.Services
{
    using HallRental.Data.Models;
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
                    string eventStart,
                    string eventEnd,
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
                    decimal securityPrice,
                    decimal totalPrice,
                    string description,
                    string caterer,
                    string eventTitle);

        bool EventExists(DateTime date, RentTimeEnum rentTime, int hallId);

        decimal CheckHallStartPrice(Hall currentHall, DayOfWeek eventDate, RentTimeEnum rentTime);

        string GetRentTimeDisplay(RentTimeEnum rentTime);

        DateTime GetStartTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

        DateTime GetEndTimeDefault(RentTimeEnum rentTime, DateTime eventDate);

    }

}
