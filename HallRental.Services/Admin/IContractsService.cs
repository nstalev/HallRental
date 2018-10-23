

namespace HallRental.Services.Admin
{
    using System;
    using System.Threading.Tasks;

    public interface IContractsService
    {
        Task<bool> SaveContractSubmission(byte[] contractSubmission, DateTime currentDate);
    }
}
