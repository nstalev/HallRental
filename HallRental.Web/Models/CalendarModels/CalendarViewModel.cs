
namespace HallRental.Web.Models.CalendarModels
{
    using HallRental.Services.Models.Halls;
    using System.Collections.Generic;

    public class CalendarViewModel
    {
        public IEnumerable<HallEventCheckModel> AllHals { get; set; }

        public int CurrentHallId { get; set; }
    }
}
