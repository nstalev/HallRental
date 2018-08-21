﻿
namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class DateCheckFormModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }
    }
}
