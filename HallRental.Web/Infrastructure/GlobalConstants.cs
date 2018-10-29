
namespace HallRental.Web.Infrastructure
{
    public class GlobalConstants
    {
        public const string AdminRole = "Admin";

        public const string TempDataSuccessMessageKey = "SuccessMessage";

        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const int MaxSecurityCuards = 20;

        public const int MaxSecurityServiceHoursPerGuard = 12;

        public const int MyEventsMaxPageSize = 5;

        public const int AdminEventsMaxPageSize = 5;

        public const int UsersListMaxPageSize = 10;


        //FOR EMAIL SENDER

        //OPERATING EMAIL

        public const string MyOperatingEmail = "";
        public const string MyOperatingEmailPassword = "";
        public const string MyOperatingEmailName = "Hall Rental";


        //for gmail
        public const string EmailProviderOperatingEmail = "smtp.gmail.com";
        public const int PortNumberOperatingEmail = 587;

        //Home email
        public const string HomeEmail = "";
    }
}
