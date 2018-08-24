
namespace HallRental.Services
{
    using HallRental.Services.Models.Halls;
    using System.Collections.Generic;

    public interface IHallsService
    {
        IEnumerable<HallEventCheckModel> AllHalls();
    }
}
