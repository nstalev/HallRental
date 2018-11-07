
namespace HallRental.Services.Admin.Models.Events
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;


    public class EditEventServiceModel
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        public int HallId { get; set; }

        [Required]
        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        [Display(Name = "Event Start")]
        public DateTime EventStart { get; set; }

        [Required]
        [Display(Name = "Event End")]
        public DateTime EventEnd { get; set; }

        [Required]
        [Display(Name = "Number of people")]
        [Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }

        [Required]
        [Display(Name = "Type Of Event")]
        public string TypeOfEvent { get; set; }

        public string Description { get; set; }

        public string Caterer { get; set; }

        [Display(Name = "Using tables and chairs")]
        public bool UsingTablesAndChairs { get; set; }

        [Display(Name = "Tables and chairs cost per person")]
        public decimal TablesAndChairsCostPerPerson { get; set; }

        [Display(Name = "Parking lot security service")]
        public bool ParkingLotSecurityService { get; set; }

        [Range(0, 16)]
        [Display(Name = "Parking Lot Security Hours")]
        public int ParkingLotSecurityHours { get; set; }

        [Display(Name = "Security start time")]
        public DateTime SecurityStartTime { get; set; }

        [Display(Name = "Security end time")]
        public DateTime SecurityEndTime { get; set; }

        [Display(Name = "Security cost per hour")]
        public decimal SecurityCostPerHour { get; set; }

        [Display(Name = "Additional Charges")]
        public decimal AdditionalCharges { get; set; }

        [Display(Name = "Additional charges info")]
        public string AdditionalChargesInformation { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Hall Rental Price")]
        public decimal HallRentalPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Tables and chairs Price")]
        public decimal TablesAndChairsPrice { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Parking lot security Price")]
        public decimal ParkingLotSecurityPrice { get; set; }

        [Display(Name = "Security Deposit")]
        public decimal SecurityDeposit { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }


    }
}
