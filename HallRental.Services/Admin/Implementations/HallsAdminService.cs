
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
                .Where(h => h.IsHallActive ==true)
                .ProjectTo<HallsListServiceModel>()
                .ToList();
        }

        public IEnumerable<HallsListServiceModel> AllDisabledHalls()
        {
            return this.db.Halls
               .Where(h => h.IsHallActive == false)
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

        public void Create(string name,
                           int hallCapacity,
                           decimal mondayFriday8amTo3pm,
                           decimal mondayThursday4pmToMN,
                           decimal friday4pmToMN,
                           decimal saturday8amTo3pm,
                           decimal saturday4pmToMN,
                           decimal sunday8amTo3pm,
                           decimal sunday4pmToMN,
                           decimal tablesAndChairsCostPerPerson,
                           decimal securityGuardCostPerHour,
                           decimal securityDepositBefore10pm,
                           decimal securityDepositAfter10pm)
        {
            Hall newHall = new Hall
            {
                Name = name,
                HallCapacity = hallCapacity,
                MondayFriday8amTo3pm = mondayFriday8amTo3pm,
                MondayThursday4pmToMN = mondayThursday4pmToMN,
                Friday4pmToMN = friday4pmToMN,
                Saturday8amTo3pm = saturday8amTo3pm,
                Saturday4pmToMN = saturday4pmToMN,
                Sunday8amTo3pm = sunday8amTo3pm,
                Sunday4pmToMN = sunday4pmToMN,
                TablesAndChairsCostPerPerson = tablesAndChairsCostPerPerson,
                SecurityGuardCostPerHour = securityGuardCostPerHour,
                SecurityDepositBefore10pm = securityDepositBefore10pm,
                SecurityDepositAfter10pm = securityDepositAfter10pm,
                IsHallActive = true
            
            };

            this.db.Halls.Add(newHall);
            this.db.SaveChanges();

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
                         decimal sunday4pmToMN,
                         decimal tablesAndChairsCostPerPerson,
                         decimal securityGuardCostPerHour,
                         decimal securityDepositBefore10pm,
                         decimal securityDepositAfter10pm)
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
            currentHall.TablesAndChairsCostPerPerson = tablesAndChairsCostPerPerson;
            currentHall.SecurityGuardCostPerHour = securityGuardCostPerHour;
            currentHall.SecurityDepositBefore10pm = securityDepositBefore10pm;
            currentHall.SecurityDepositAfter10pm = securityDepositAfter10pm;

            this.db.SaveChanges();

        }

        public void DisableHall(int id)
        {
            Hall currentHall = this.db.Halls.Find(id);

            currentHall.IsHallActive = false;
            this.db.SaveChanges();
        }

        public void EnableHall(int id)
        {
            Hall currentHall = this.db.Halls.Find(id);

            currentHall.IsHallActive = true;
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
