﻿

namespace HallRental.Services.Models.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class EventDetailsServiceModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string HallName { get; set; }

        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public string RentTimeDisplay { get; set; }

        public DateTime EventStart { get; set; }

        public DateTime EventEnd { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public int NumberOfPeople { get; set; }

        public string TypeOfEvent { get; set; }

        public string Description { get; set; }

        public string Caterer { get; set; }

        public bool UsingTablesAndChairs { get; set; }

        public decimal TablesAndChairsCostPerPerson { get; set; }

        public int ParkingLotSecurityHours { get; set; }

        public DateTime SecurityStartTime { get; set; }

        public DateTime SecurityEndTime { get; set; }

        public decimal SecurityCostPerHour { get; set; }

        [Display(Name = "Additional Charges")]
        public decimal AdditionalCharges { get; set; }

        [Display(Name = "Additional Charges Info")]
        public string AdditionalChargesInformation { get; set; }

        public decimal HallRentalPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public bool ParkingLotSecurityService { get; set; }

        public decimal ParkingLotSecurityPrice { get; set; }

        public decimal SecurityDeposit { get; set; }

        public decimal TotalPrice { get; set; }


        public string TenantId { get; set; }


        public bool IsReservationConfirmed { get; set; }
    }
}
