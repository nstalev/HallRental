
namespace HallRental.Services.Admin
{
    using HallRental.Services.Admin.Models.Halls;
    using System.Collections.Generic;

    public interface IHallsAdminService
    {
        IEnumerable<HallsListServiceModel> AllActiveHalls();

        IEnumerable<HallsListServiceModel> AllDisabledHalls();

        HallServiceModel ById (int id);

        HallsFormServiceModel GetFormModelById(int id);

        bool Exists(int id);

        void Edit(int id, string name, int hallCapacity, decimal mondayFriday8amTo3pm, decimal mondayThursday4pmToMN, decimal friday4pmToMN, decimal saturday8amTo3pm, decimal saturday4pmToMN, decimal sunday8amTo3pm, decimal sunday4pmToMN,decimal tablesAndChairsCostPerPerson, decimal securityGuardCostPerHour);

        void Create(string name, int hallCapacity, decimal mondayFriday8amTo3pm, decimal mondayThursday4pmToMN, decimal friday4pmToMN, decimal saturday8amTo3pm, decimal saturday4pmToMN, decimal sunday8amTo3pm, decimal sunday4pmToMN, decimal tablesAndChairsCostPerPerson, decimal securityGuardCostPerHour);

        void DisableHall(int id);
        void EnableHall(int id);
    }
}
