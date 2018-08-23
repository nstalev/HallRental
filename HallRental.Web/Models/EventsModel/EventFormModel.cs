

namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class EventFormModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public RentTimeEnum RentTime { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "NumberOfPeople must be positive number")]
        public int NumberOfPeople { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }
    }
}
