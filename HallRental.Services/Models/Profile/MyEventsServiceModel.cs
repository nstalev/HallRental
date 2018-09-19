
namespace HallRental.Services.Models.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class MyEventsServiceModel
    {
        public int EventId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        public string HallName { get; set; }

        public string EventTitle { get; set; }

        public bool IsReservationConfirmed { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public int NumberOfPeople { get; set; }

        public decimal Totalprice { get; set; }

    }
}
