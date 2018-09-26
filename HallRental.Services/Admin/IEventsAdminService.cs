
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models.Events;
    using System.Collections.Generic;

    public interface IEventsAdminService
    {
        IEnumerable<EventsListServiceModel> GetEventRequests(string search, int page, int pageSize);

        int TotalEvetRequests(string search);
    }
}
