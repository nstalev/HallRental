

namespace HallRental.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class Event
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public  string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public int NumberOfPeople { get; set; }

        [Required]
        public string EventTitle { get; set; }

        public string Description { get; set; }

        public bool IsConfirmed { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        public int HallId { get; set; }

        public Hall Hall { get; set; }

    }
}
