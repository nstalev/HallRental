
namespace HallRental.Services.Models.Events
{
    using System;
    using static HallRental.Data.Enums.Enums;

    public class EventCalendarServiceModel
    {
        public string Title { get; set; }
        public string Descrtiption { get; set; }

        public DateTime Start { get; set; }

        public RentTimeEnum RentTime { get; set; }

       // public string Color { get; set; }

    }
}
