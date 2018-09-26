
namespace HallRental.Web.Areas.Admin.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventsListModel
    {
        public int EventId { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string HallName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string RentTimeDisplay { get; set; }

        public int NumberOfPeople { get; set; }

        public bool IsReservationConfirmed { get; set; }

        public decimal Totalprice { get; set; }
    }
}
