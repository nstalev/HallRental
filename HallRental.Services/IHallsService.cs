
namespace HallRental.Services
{
    using HallRental.Data.Models;
    using HallRental.Services.Models.Halls;
    using System.Collections.Generic;

    public interface IHallsService
    {
        IEnumerable<HallEventCheckModel> AllHalls();

        IEnumerable<HallPriceListServiceModel> AllHallsPriceList();

        Hall GetHallById (int hallId);

        bool HallExists(int hallId);
    }

}
