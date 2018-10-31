namespace HallRental.Web.Models.EventsModel
{
    using HallRental.Web.Infrastructure;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;


    public class EventInfoAndPriceCheckViewModel
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public long EventStartDateTimeInMs { get; set; }
        public long EventEndDateTimeInMs { get; set; }

        public long TestTimeInMs { get; set; }


        public int HallId { get; set; }

        public string HallName { get; set; }

        public string RentTimeDisplay { get; set; }

        [Required]
        public RentTimeEnum RentTime { get; set; }

        

        [Display(Name = "Event Start")]
        public DateTime EventStart { get; set; }

        [Display(Name = "Event End")]
        public DateTime EventEnd { get; set; }


        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please mark a number of people")]
        [Display(Name = "Number of People")]
        public int NumberOfPeople { get; set; }

        public bool UsingTablesAndChairs { get; set; }


        public int HallCapacity { get; set; }


        [Display(Name = "Parking Lot Security Service")]
        public bool ParkingLotSecurityService { get; set; }


        public int ParkingLotSecurityHours { get; set; }

        [Display(Name = "Security Start Time")]
        public DateTime SecurityStartTime { get; set; }

        [Display(Name = "Security End Time")]
        public DateTime SecurityEndTime { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        public decimal ChairTableCostPerPerson { get; set; }

        public decimal HallRentalPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal ParkingLotSecurityPrice { get; set; }


        public decimal TotalPrice { get; set; }

        public EventPriceModel EventPriceModel { get; set; }


    }
}
