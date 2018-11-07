
namespace HallRental.Web.Models.ProfileViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MyEventsListModel
    {
        public int EventId { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public string HallName { get; set; }

        public string TypeOfEvent { get; set; }

        public string RentTimeDisplay { get; set; }

        public int NumberOfPeople { get; set; }

        public bool IsConfirmed { get; set; }

        public decimal Totalprice { get; set; }
    }
}
