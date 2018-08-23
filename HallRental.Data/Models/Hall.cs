
namespace HallRental.Data.Models
{
    using System.Collections.Generic;

    public class Hall
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

        public List<Event> Events { get; set; } = new List<Event>();

    }
}
