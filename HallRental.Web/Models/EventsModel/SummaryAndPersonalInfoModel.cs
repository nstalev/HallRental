﻿

namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class SummaryAndPersonalInfoModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }


        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        public string EventTitle { get; set; }


        [DataType(DataType.Time)]
        public DateTime EventStart { get; set; }

        [DataType(DataType.Time)]
        public DateTime EventEnd { get; set; }


        [Required]
        public int NumberOfPeople { get; set; }

        public bool UsingTablesAndChairs { get; set; }


        public decimal SecurityGuardCostPerHour { get; set; }

        public int SecurityGuards { get; set; }

        public double RequestedSecurityHoursPerGuard { get; set; }

        public decimal HallRentalPrice { get; set; }


        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }


        public decimal TotalPrice { get; set; }
    }
}
