
namespace HallRental.Web.Models.EventsModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static HallRental.Data.Enums.Enums;

    public class SummaryAndPerInfoVM
    {

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int HallId { get; set; }

        public string HallName { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public string RentTimeDisplay { get; set; }

        public string EventTitle { get; set; }

        [DataType(DataType.Time)]
        public DateTime? EventStart { get; set; }

        [DataType(DataType.Time)]
        public DateTime? EventEnd { get; set; }

        public int NumberOfPeople { get; set; }

        public bool UseTablesAndChairs { get; set; }


        public int SecurityGuards { get; set; }

        public double SecurityServiceHoursPerGuard { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        public decimal HallRentPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }


        public decimal TotalPrice { get; set; }
    }
}
