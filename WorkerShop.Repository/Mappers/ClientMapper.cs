using AutoMapper;
using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.Mappers
{
    public class ClientMapper : Profile
    {
        public ClientMapper() 
        {
            //overall api
            CreateMap<ClientModel, ClientDTO>()
                .ForMember(dest => dest.Vehicles, temp => temp.MapFrom(src => src.Vehicles));
            CreateMap<ClientDTO, Client>()
                .ForMember(dest => dest.Vehicles, temp => temp.MapFrom(src => src.Vehicles));

            //restricting one to many
            CreateMap<ClientModel, ClientWithoutVehiclesDTO>();
            CreateMap<ClientWithoutVehiclesDTO, Client>()
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore());

            //patch
            CreateMap<PatchClientDTO, Client>()
                .ForMember(dest => dest.Vehicles, opt => opt.Ignore());
            CreateMap<Client, PatchClientDTO>();

            //adding address property
            CreateMap<Client,ClientWithAdressDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                                                   src.ApartmentNumber.HasValue
                                                   ? $"{src.Street} {src.BuildingNumber}/{src.ApartmentNumber}, {src.City}"
                                                   : $"{src.Street} {src.BuildingNumber}, {src.City}"));

        }
    }
}
