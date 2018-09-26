
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models.Events;
    using HallRental.Services.Models.Profile;
    using System;
    using System.Collections.Generic;

    public interface IEventsAdminService
    {
        IEnumerable<EventsListServiceModel> GetEventRequests(string search, int page, int pageSize);

        int TotalEvetRequests(string search);


        IEnumerable<EventsListServiceModel> GetConfirmedUpcomingEvents(string search, int page, int pageSize, DateTime currentDate);

        int TotalConfirmedUpcomingEvents(string search, DateTime currentDate);

        EventDetailsServiceModel EventById(int id);

        bool EventExists(int id);

        void ConfirmEvent(int id);
    }
}
