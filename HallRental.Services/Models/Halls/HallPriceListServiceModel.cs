namespace HallRental.Services.Models.Halls
{
    using System.ComponentModel.DataAnnotations;

    public class HallPriceListServiceModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Monday to Friday 8:00 am to 3:00 pm")]
        public decimal MondayFriday8amTo3pm { get; set; }

        [Required]
        [Display(Name = "Monday to Thursday 4:00 pm to Midnight")]
        public decimal MondayThursday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Friday 4:00 pm to Midnight")]
        public decimal Friday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Saturday 8:00 am to 3:00 pm")]
        public decimal Saturday8amTo3pm { get; set; }

        [Required]
        [Display(Name = "Saturday 4:00 pm to Midnight")]
        public decimal Saturday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Sunday 8:00 am to 3:00 pm")]
        public decimal Sunday8amTo3pm { get; set; }

        [Required]
        [Display(Name = "Sunday 4:00 pm to Midnight")]
        public decimal Sunday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Hall Capacity")]
        public int HallCapacity { get; set; }

        [Required]
        [Display(Name = "Tables and chairs cost by person")]
        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsCostPerPerson { get; set; }

        [Required]
        [Display(Name = "Security Guard service cost per hour")]
        public decimal SecurityGuardCostPerHour { get; set; }

        [Required]
        [Display(Name = "Security Deposit Before 10 pm")]
        public decimal SecurityDepositBefore10pm { get; set; }

        [Required]
        [Display(Name = "Security Deposit After 10 pm")]
        public decimal SecurityDepositAfter10pm { get; set; }
    }
}
