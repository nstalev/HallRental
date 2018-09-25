
namespace HallRental.Services.Admin.Models.Halls
{
    using System.ComponentModel.DataAnnotations;

    public class HallsFormServiceModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Monday to Friday 8:00 am to 3:00 pm")]
        [Range(0, double.MaxValue)]
        public decimal MondayFriday8amTo3pm { get; set; }

        [Required]
        [Display(Name = "Monday to Thursday 4:00 pm to Midnight")]
        [Range(0, double.MaxValue)]
        public decimal MondayThursday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Friday 4:00 pm to Midnight")]
        [Range(0, double.MaxValue)]
        public decimal Friday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Saturday 8:00 am to 3:00 pm")]
        [Range(0, double.MaxValue)]
        public decimal Saturday8amTo3pm { get; set; }

        [Required]
        [Display(Name= "Saturday 4:00 pm to Midnight")]
        public decimal Saturday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Sunday 8:00 am to 3:00 pm")]
        [Range(0, double.MaxValue)]
        public decimal Sunday8amTo3pm { get; set; }

        [Required]
        [Display(Name = "Sunday 4:00 pm to Midnight")]
        [Range(0, double.MaxValue)]
        public decimal Sunday4pmToMN { get; set; }

        [Required]
        [Display(Name = "Hall Capacity")]
        [Range(1, int.MaxValue)]
        public int HallCapacity { get; set; }

        [Required]
        [Display(Name = "Tables and chairs cost by person")]
        [Range(0, double.MaxValue)]
        public decimal TablesAndChairsCostPerPerson { get; set; }

        [Required]
        [Display(Name = "Security Guard service cost per hour")]
        [Range(0, double.MaxValue)]
        public decimal SecurityGuardCostPerHour { get; set; }
    }
}
