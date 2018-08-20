using System;
using System.ComponentModel.DataAnnotations;
using static HallRental.Data.Enums.Enums;

namespace HallRental.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public  string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfPeople { get; set; }

        public string EventTitle { get; set; }

        public string Description { get; set; }

        public bool IsConfirmed { get; set; }

    }
}
