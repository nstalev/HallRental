
namespace HallRental.Web.Models.ManageViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class IndexViewModel
    {
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public string StatusMessage { get; set; }
    }
}
