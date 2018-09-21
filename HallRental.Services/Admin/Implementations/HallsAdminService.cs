

namespace HallRental.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Services.Admin.Models.Halls;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HallsAdminService : IHallsAdminService
    {
        private readonly HallRentalDbContext db;

        public HallsAdminService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<HallsListServiceModel> AllActiveHalls()
        {
            return this.db.Halls
                .ProjectTo<HallsListServiceModel>()
                .ToList();
        }

        public HallDetailsServiceModel GetHallById(int id)
        {
            return this.db.Halls
                    .Where(h => h.Id == id)
                    .ProjectTo<HallDetailsServiceModel>()
                    .FirstOrDefault();
        }   
    }
}
