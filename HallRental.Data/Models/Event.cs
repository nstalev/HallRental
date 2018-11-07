

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
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        public DateTime EventStart { get; set; }

        [Required]
        public DateTime EventEnd { get; set; }

        [MaxLength(20)]
        [MinLength(3)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(5)]
        public string FullName { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfPeople { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string TypeOfEvent { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(400)]
        public string Caterer { get; set; }


        public bool UsingTablesAndChairs { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsCostPerPerson { get; set; }

        public bool ParkingLotSecurityService { get; set; }

        [Range(0, 16)]
        public int ParkingLotSecurityHours { get; set; }

        public DateTime SecurityStartTime { get; set; }

        public DateTime SecurityEndTime { get; set; }

        public decimal SecurityCostPerHour { get; set; }

        public decimal AdditionalCharges { get; set; }

        public string AdditionalChargesInformation { get; set; }

        [Required]
        public decimal HallRentalPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsPrice { get; set; }


        [Range(0, double.MaxValue)]
        public decimal ParkingLotSecurityPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SecurityDeposit { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public int HallId { get; set; }

        public Hall Hall { get; set; }

        public string TenantId { get; set; }

        public User Tenant { get; set; }

        public bool IsReservationConfirmed { get; set; }

    }
}
