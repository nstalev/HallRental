
namespace HallRental.Services.Admin.Models.Events
{
    using System;
    using System.Collections.Generic;
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
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
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
        public string EventTitle { get; set; }

        public string Description { get; set; }

        public string Caterer { get; set; }

        [Display(Name = "Using tables and chairs")]
        public bool UsingTablesAndChairs { get; set; }

        public decimal TablesAndChairsCostPerPerson { get; set; }

        public bool ParkingLotSecurityService { get; set; }

        [Range(0, 16)]
        [Display(Name = "Parking Lot Security Hours")]
        public int ParkingLotSecurityHours { get; set; }

        public DateTime SecurityStartTime { get; set; }

        public DateTime SecurityEndTime { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        [Range(0, double.MaxValue)]
        public decimal HallRentalPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal ParkingLotSecurityPrice { get; set; }

        public decimal SecurityDeposit { get; set; }

        public decimal TotalPrice { get; set; }


    }
}
