
namespace HallRental.Services.Admin.Models.Events
{
    using static HallRental.Data.Enums.Enums;

    public class EvenAlertNotificationSM
    {
        public int Id { get; set; }

        public string HallName { get; set; }

        public RentTimeEnum RentTime { get; set; }

        public string FullName { get; set; }

        public bool IsReservationConfirmed { get; set; }

    }
}
