
namespace HallRental.Services.Admin.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class EventsListServiceModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string HallName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public int NumberOfPeople { get; set; }

        public bool IsReservationConfirmed { get; set; }

        public decimal Totalprice { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurrentDate { get; set; }
    }
}
