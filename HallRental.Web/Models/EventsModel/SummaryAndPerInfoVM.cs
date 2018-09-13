
namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class SummaryAndPerInfoVM
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        public string HallName { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public string RentTimeDisplay { get; set; }

        public string EventTitle { get; set; }

        public string EventStart { get; set; }

        public string EventEnd { get; set; }

        public int NumberOfPeople { get; set; }

        public bool UsingTablesAndChairs { get; set; }


        public int SecurityGuards { get; set; }

        public double RequestedSecurityHoursPerGuard { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        public decimal HallRentalPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }

        public decimal TotalPrice { get; set; }

        //Personal Information

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public string EventDescription { get; set; }

        public string Caterer { get; set; }

    }
}
