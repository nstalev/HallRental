
namespace HallRental.Web.Models.EventsModel
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class DateCheckFormModel
    {
        [Required]
        public int HallId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        public decimal TotalPrice { get; set; } = 0;
    }
}
