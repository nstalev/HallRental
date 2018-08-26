
namespace HallRental.Services
{
    using HallRental.Services.Models.Events;
    using System.Collections.Generic;

    public interface ICalendarService
    {

        IEnumerable<EventCalendarServiceModel> AllEvents(int hallId);
    }
}
