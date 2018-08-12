
namespace HallRental.Services
{
    using HallRental.Services.Models;
    using System.Collections.Generic;

    public interface IIdentityService
    {
        IEnumerable<UserModel> AllUsers();
    }
}
