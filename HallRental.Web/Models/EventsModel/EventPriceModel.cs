
namespace HallRental.Web.Models.EventsModel
{
    public class EventPriceModel
    {
        public decimal HallPrice { get; set; }

        public decimal TablesAndChairsPrice { get; set; }

        public decimal SecurityPrice { get; set; }

        public decimal SecurityDeposit { get; set; }

        public decimal TotalPrice { get; set; }

        public int ParkingLotSecurityHours { get; set; }


    }
}
