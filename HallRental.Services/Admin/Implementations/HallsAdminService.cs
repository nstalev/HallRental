

namespace HallRental.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Models;
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

       

        public HallServiceModel ById(int id)
        {
            return this.db.Halls
                    .Where(h => h.Id == id)
                    .ProjectTo<HallServiceModel>()
                    .FirstOrDefault();
        }

        public void Edit(int id,
                         string name,
                         int hallCapacity,
                         decimal mondayFriday8amTo3pm,
                         decimal mondayThursday4pmToMN,
                         decimal friday4pmToMN,
                         decimal saturday8amTo3pm,
                         decimal saturday4pmToMN,
                         decimal sunday8amTo3pm,
                         decimal sunday4pmToMN)
        {
            Hall currentHall = this.db.Halls.Find(id);

            if (currentHall ==null)
            {
                return;
            }

            currentHall.Name = name;
            currentHall.HallCapacity = hallCapacity;
            currentHall.MondayFriday8amTo3pm = mondayFriday8amTo3pm;
            currentHall.MondayThursday4pmToMN = mondayThursday4pmToMN;
            currentHall.Friday4pmToMN = friday4pmToMN;
            currentHall.Saturday8amTo3pm = saturday8amTo3pm;
            currentHall.Saturday4pmToMN = saturday4pmToMN;
            currentHall.Sunday8amTo3pm = sunday8amTo3pm;
            currentHall.Sunday4pmToMN = sunday4pmToMN;

            this.db.SaveChanges();

        }

        public bool Exists(int id)
        {
            return this.db.Halls.Any(h => h.Id == id);
        }

        public HallsFormServiceModel GetFormModelById(int id)
        {
            return this.db.Halls
                  .Where(h => h.Id == id)
                  .ProjectTo<HallsFormServiceModel>()
                  .FirstOrDefault();
        }
    }
}
