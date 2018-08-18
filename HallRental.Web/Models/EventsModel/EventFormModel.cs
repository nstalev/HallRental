

namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EventFormModel
    {
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public DateTime EventStart { get; set; }

        [Required]
        public DateTime EventEdn { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "NumberOfPeople must be positive number")]
        public int NumberOfPeople { get; set; }

        [Required]
        public string EventTitle { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
