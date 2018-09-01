

namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class PersonalInformationViewModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }


        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        public string EventTitle { get; set; }


        [DataType(DataType.Time)]
        public DateTime? EventStart { get; set; }

        [DataType(DataType.Time)]
        public DateTime? EventEnd { get; set; }


        [Required]
        public int NumberOfPeople { get; set; }

        public bool UseTablesAndChairs { get; set; }


        public bool SecurityService { get; set; }

        public int SecurityServiceHours { get; set; }

        public decimal Price { get; set; }
    }
}
