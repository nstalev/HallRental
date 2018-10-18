
namespace HallRental.Services.Admin.Models.Halls
{
    public class HallServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal MondayFriday8amTo3pm { get; set; }

      
        public decimal MondayThursday4pmToMN { get; set; }

        
        public decimal Friday4pmToMN { get; set; }

       
        public decimal Saturday8amTo3pm { get; set; }

        public decimal Saturday4pmToMN { get; set; }

       
        public decimal Sunday8amTo3pm { get; set; }

      
        public decimal Sunday4pmToMN { get; set; }

        
        public int HallCapacity { get; set; }

        public decimal TablesAndChairsCostPerPerson { get; set; }

        public decimal SecurityGuardCostPerHour { get; set; }

        public decimal SecurityDepositBefore10pm { get; set; }

        public decimal SecurityDepositAfter10pm { get; set; }
    }
}
