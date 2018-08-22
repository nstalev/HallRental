
namespace HallRental.Data.Enums
{
    using System.ComponentModel.DataAnnotations;

    public class Enums
    {

        public enum RentTimeEnum
        {
            [Display(Name = "8:00 am to 3:00 pm")]
            eightAMtoThreePM= 1,


            [Display(Name = "4:00 pm to Midnight")]
            forPMtoMidNight = 2,

            [Display(Name = "AllDay")]
            allDay = 3


        }
    }
}
