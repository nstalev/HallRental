using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HallRental.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public  string Email { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEdn { get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfPeople { get; set; }

        public string EventTitle { get; set; }

        public string Description { get; set; }

        public bool IsConfirmed { get; set; }

    }
}
