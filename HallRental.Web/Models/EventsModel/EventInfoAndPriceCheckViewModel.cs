﻿namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;


    public class EventInfoAndPriceCheckViewModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        public string HallName { get; set; }

        public string RentTimeDisplay { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        [Required]
        [Display(Name ="Event Title")]
        public string EventTitle { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Event Start")]
        public DateTime? EventStart { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Event End")]
        public DateTime? EventEnd { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please mark a number of people")]
        [Display(Name = "Number of People")]
        public int NumberOfPeople { get; set; }

        public bool UseTablesAndChairs { get; set; }


        public bool SecurityService { get; set; }

        public int SecurityServiceHours { get; set; }

        public decimal Price { get; set; }

    }
}
