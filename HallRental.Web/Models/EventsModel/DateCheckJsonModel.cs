
namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class DateCheckJsonModel
    {
        [DataType(DataType.Date)]
        [Required]
        public DateTime Date { get; set; }

        public RentTimeEnum RentTime { get; set; }

        [Required]
        public int HallId { get; set; }


    }
}
