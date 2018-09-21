
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models.Halls;
    using System.Collections.Generic;

    public interface IHallsAdminService
    {
        IEnumerable<HallsListServiceModel> AllActiveHalls();

        HallDetailsServiceModel GetHallById(int id);
    }
}
