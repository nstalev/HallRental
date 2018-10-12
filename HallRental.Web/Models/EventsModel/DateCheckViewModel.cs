
namespace HallRental.Web.Models.EventsModel
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class DateCheckViewModel
    {
        public IEnumerable<SelectListItem> Halls { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        public long CurrentDateTimeInMs { get; set; }
    }
}
