
namespace HallRental.Services.Admin.Implementations
{
    using HallRental.Data;
    using HallRental.Data.Models;
    using System;
    using System.Threading.Tasks;

    public class ContractsService : IContractsService
    {
        private readonly HallRentalDbContext db;

        public ContractsService(HallRentalDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> SaveContractSubmission(byte[] contractSubmission, DateTime currentDate)
        {
            var contract = new Contract
            {
                DateSubmitted = currentDate,
                ContractSubmission = contractSubmission
            };

            this.db.Contracts.Add(contract);

            await this.db.SaveChangesAsync();

            return true;
        }
    }
}
