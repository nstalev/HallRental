
namespace HallRental.Services
{
    using HallRental.Services.Models.Profile;
    using System.Collections.Generic;

    public interface IProfileService
    {
        IEnumerable<MyEventsServiceModel> MyEvents(string userId);
    }
}
