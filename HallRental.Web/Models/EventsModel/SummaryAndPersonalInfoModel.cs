

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
        public DateTime EventStart { get; set; }

        [Required]
        public DateTime EventEnd { get; set; }


        [Required]
        public int NumberOfPeople { get; set; }

        public bool UsingTablesAndChairs { get; set; }


        public decimal SecurityGuardCostPerHour { get; set; }

        public bool ParkingLotSecurityService { get; set; }


        public int ParkingLotSecurityHours { get; set; }

        [Display(Name = "Security Start Time")]
        public DateTime SecurityStartTime { get; set; }

        [Display(Name = "Security End Time")]
        public DateTime SecurityEndTime { get; set; }

        public decimal HallRentalPrice { get; set; }


        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }


        public decimal TotalPrice { get; set; }
    }
}
