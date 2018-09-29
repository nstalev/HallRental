
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models;
    using System.Collections.Generic;

    public interface IIdentityService
    {
        void DeleteEvents(string id);

        int AllUsersCount(string search);

        IEnumerable<UserModel> GetUsers(string search, int page, int pageSize);
    }
}
