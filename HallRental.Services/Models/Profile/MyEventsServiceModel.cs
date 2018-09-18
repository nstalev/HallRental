
namespace HallRental.Services.Models.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MyEventsServiceModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        public bool IsReservationConfirmed { get; set; }
    }
}
