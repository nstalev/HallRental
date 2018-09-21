using HallRental.Services.Admin.Models.Halls;
using System;
using System.Collections.Generic;
using System.Text;

namespace HallRental.Services.Admin
{
    public interface IHallsAdminService
    {
        IEnumerable<HallsListServiceModel> AllActiveHalls();
    }
}
