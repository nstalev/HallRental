
namespace HallRental.Services
{
    using HallRental.Services.Models.Profile;
    using System;
    using System.Collections.Generic;

    public interface IProfileService
    {
        IEnumerable<MyEventsServiceModel> MyFutureEvents(string userId, int page, int pageSize, DateTime markDate);

        int TotalFutureEvents(string userId, DateTime markDate);

        IEnumerable<MyEventsServiceModel> MyPassedEvents(string userId, int page, int pageSize, DateTime markDate);

        int TotalPassedEvents(string userId, DateTime markDate);
    }
}
