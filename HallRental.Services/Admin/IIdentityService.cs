
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models;
    using System.Collections.Generic;

    public interface IIdentityService
    {
        IEnumerable<UserModel> AllUsers();

        void DeleteEvents(string id);
    }
}
