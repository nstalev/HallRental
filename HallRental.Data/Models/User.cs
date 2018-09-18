
namespace HallRental.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        public List<Event> Events { get; set; } = new List<Event>();
    }
}
