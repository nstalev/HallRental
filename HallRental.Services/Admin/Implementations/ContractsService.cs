
namespace HallRental.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using HallRental.Data;
    using HallRental.Data.Models;
    using HallRental.Services.Admin.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ContractsService : IContractsService
    {
        private readonly HallRentalDbContext db;

        public ContractsService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public bool ContractExists(int id)
        {
            return this.db.Contracts
                .Any(c => c.Id == id);
        }

        public IEnumerable<RentalContractServiceModel> GetAllContracts()
        {
            return this.db.Contracts
                .ProjectTo<RentalContractServiceModel>()
                .ToList();
        }

        public RentalContractServiceModel GetContractSubmissionById(int id)
        {
            var currentContract = this.db.Contracts
                .Where(c => c.Id == id)
                .ProjectTo<RentalContractServiceModel>()
                .FirstOrDefault();

            if (currentContract == null)
            {
                return null;
            }

            return currentContract;
        }

        public async Task<bool> SaveContractSubmission(byte[] contractSubmission, DateTime currentDate, string fileName)
        {
            var contract = new RentalContract
            {
                DateSubmitted = currentDate,
                ContractSubmission = contractSubmission,
                Name = fileName
            };

            this.db.Contracts.Add(contract);

            await this.db.SaveChangesAsync();

            return true;
        }


        public bool DeleteContract(int id)
        {
            var contractForDelete = this.db.Contracts.Find(id);

            if (contractForDelete == null)
            {
                return false;
            }

            this.db.Contracts.Remove(contractForDelete);
            this.db.SaveChanges();

            return true;
        }
    }
}
