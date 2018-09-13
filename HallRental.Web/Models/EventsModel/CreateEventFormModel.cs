

namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class CreateEventFormModel
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [Display(Name = "Phone Number")]
        public string Email { get; set; }


        public string EventStart { get; set; }

        public string EventEnd { get; set; }

        public int NumberOfPeople { get; set; }

        //TablesAndChairs
        public bool UsingTablesAndChairs { get; set; }
       
        public decimal TablesAndChairsCostPerPerson { get; set; }


        //Security
        public decimal SecurityGuardCostPerHour { get; set; }

        public int SecurityGuards { get; set; }

        public double RequestedSecurityHoursPerGuard { get; set; }


        //Price
        public decimal HallRentalPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }

        public decimal TotalPrice { get; set; }



        public string EventDescription { get; set; }

        public string Caterer { get; set; }
    }
}
