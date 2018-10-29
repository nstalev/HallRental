
namespace HallRental.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Models;
    using HallRental.Services.Models.Halls;

    public class HallsService : IHallsService
    {
        private readonly HallRentalDbContext db;

        public HallsService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<HallPriceListServiceModel> AllHallsPriceList()
        {
            return this.db.Halls
                .Where(h => h.IsHallActive == true)
                .ProjectTo<HallPriceListServiceModel>()
                .AsQueryable();

        }

        public IEnumerable<HallEventCheckModel> AllHalls()
        {
            return this.db.Halls
                .Where(h => h.IsHallActive == true)
                .Select(h => new HallEventCheckModel
                {
                    Id = h.Id,
                    Name = h.Name
                })
                .ToList();
               
        }

        public  Hall GetHallById(int hallId)
        {
            return this.db.Halls.Find(hallId);
                
        }

        public bool HallExists(int hallId)
        {
            return this.db.Halls.Any(h => h.Id == hallId);
        }
    }
}
