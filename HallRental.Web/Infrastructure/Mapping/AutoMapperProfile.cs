﻿
namespace HallRental.Web.Infrastructure.Mapping
{
    using AutoMapper;
    using HallRental.Data.Models;
    using HallRental.Services.Admin.Models;
    using HallRental.Services.Admin.Models.Contracts;
    using HallRental.Services.Admin.Models.Events;
    using HallRental.Services.Admin.Models.Halls;
    using HallRental.Services.Models.Halls;
    using HallRental.Services.Models.Profile;

    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {

            this.CreateMap<Event, MyEventsServiceModel>()
              .ForMember(e => e.HallName, cfg => cfg.MapFrom(h => h.Hall.Name));

            this.CreateMap<Event, EventDetailsServiceModel>()
             .ForMember(e => e.HallName, cfg => cfg.MapFrom(h => h.Hall.Name));

            this.CreateMap<Event, EventDetailsAdminSM>()
            .ForMember(e => e.HallName, cfg => cfg.MapFrom(h => h.Hall.Name))
            .ForMember(e => e.UserName, cfg => cfg.MapFrom(u => u.Tenant.UserName));

            this.CreateMap<Event, EventsListServiceModel>()
            .ForMember(e => e.HallName, cfg => cfg.MapFrom(h => h.Hall.Name));

            this.CreateMap<Event, EditEventServiceModel>();


            this.CreateMap<Hall, HallsListServiceModel>();

            this.CreateMap<Hall, HallServiceModel>();

            this.CreateMap<Hall, HallsFormServiceModel>();

            this.CreateMap<Hall, HallPriceListServiceModel>();
            
            this.CreateMap<User, UserModel>();

            this.CreateMap<RentalContract, RentalContractServiceModel>();
        }
    }
}
