
namespace HallRental.Services
{
    using HallRental.Data.Models;
    using HallRental.Services.Models.Halls;
    using System.Collections.Generic;

    public interface IHallsService
    {
        IEnumerable<HallEventCheckModel> AllHalls();

        Hall GetHallById (int hallId);
    }

}
