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

        public string EventStart { get; set; }

        public string EventEnd { get; set; }

        public string PhoneNumber { get; set; }

        public string FullName { get; set; }

        public int NumberOfPeople { get; set; }

        public string EventTitle { get; set; }

        public string Description { get; set; }

        public string Caterer { get; set; }

        public bool UsingTablesAndChairs { get; set; }

        public decimal TablesAndChairsCostPerPerson { get; set; }

        public int SecurityGuards { get; set; }

        public double RequestedSecurityHoursPerGuard { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        public decimal HallRentalPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }

        public decimal TotalPrice { get; set; }


        public string TenantId { get; set; }


        public bool IsReservationConfirmed { get; set; }
    }
}
