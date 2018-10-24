

namespace HallRental.Services.Admin
{
    using HallRental.Data.Models;
    using HallRental.Services.Admin.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IContractsService
    {
        Task<bool> SaveContractSubmission(byte[] contractSubmission, DateTime currentDate, string fileName);
        IEnumerable<RentalContractServiceModel> GetAllContracts();

        bool ContractExists(int id);

        RentalContractServiceModel GetContractSubmissionById(int id);

        bool DeleteContract(int id);
    }
}
