﻿

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

        [MaxLength(20)]
        public string EventStart { get; set; }

        [MaxLength(20)]
        public string EventEnd { get; set; }

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
        public string EventTitle { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(400)]
        public string Caterer { get; set; }


        public bool UsingTablesAndChairs { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsCostPerPerson { get; set; }

        [Required]
        [Range(0, 100)]
        public int SecurityGuards { get; set; }

        [Range(0, 12)]
        public double RequestedSecurityHoursPerGuard { get; set; }


        public decimal SecurityGuardCostPerHour { get; set; }

        [Required]
        public decimal HallRentalPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsPrice { get; set; }

        [Range(0, double.MaxValue)]
        public decimal SecurityPrice { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TotalPrice { get; set; }

        public int HallId { get; set; }

        public Hall Hall { get; set; }

        public bool IsReservationConfirmed { get; set; }

    }
}
