
namespace HallRental.Web.Models.ProfileViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MyEventsViewModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string HallName { get; set; }

        public string RentTimeDisplay { get; set; }

        public int NumberOfPeople { get; set; }

        public bool IsConfirmed { get; set; }

        public decimal Totalprice { get; set; }
    }
}
