
namespace HallRental.Web.Models.IdentityViewModels
{
    using System.Collections.Generic;

    public class UserWithRolesViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
