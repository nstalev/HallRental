
namespace HallRental.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using HallRental.Data.Models;
    using HallRental.Services.Models.Profile;

    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            this.CreateMap<Event, MyEventsServiceModel>()
              .ForMember(e => e.HallName, cfg => cfg.MapFrom(h => h.Hall.Name));
        }
    }
}
