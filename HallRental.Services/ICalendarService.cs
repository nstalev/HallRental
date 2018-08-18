
namespace HallRental.Services
{
    using HallRental.Services.Models;
    using System.Collections.Generic;

    public interface ICalendarService
    {

        IEnumerable<EventServiceModel> AllEvents();
    }
}
