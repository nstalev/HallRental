
namespace HallRental.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Hall
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MondayFriday8amTo3pm { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal MondayThursday4pmToMN { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Friday4pmToMN { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Saturday8amTo3pm { get; set; }

        [Required]
        public decimal Saturday4pmToMN { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Sunday8amTo3pm { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Sunday4pmToMN { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int HallCapacity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ChairTablePerPersonCost { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SecurityGuardCostPerHour { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();

    }
}
