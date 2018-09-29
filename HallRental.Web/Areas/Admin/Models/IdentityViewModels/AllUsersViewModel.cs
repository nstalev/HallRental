
namespace HallRental.Web.Areas.Admin.Models.IdentityViewModels
{
    using HallRental.Services.Admin.Models;
    using System.Collections.Generic;

    public class AllUsersViewModel
    {
        public IEnumerable<UserModel> AllUsers { get; set; }

        public string Search { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
