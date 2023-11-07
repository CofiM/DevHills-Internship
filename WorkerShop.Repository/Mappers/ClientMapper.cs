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
            CreateMap<ClientModel, ClientDTO>()
                .ForMember(dest => dest.Vehicles, temp => temp.MapFrom(src => src.Vehicles));
            CreateMap<ClientDTO, Client>()
                .ForMember(dest => dest.Vehicles, temp => temp.MapFrom(src => src.Vehicles));
        }
    }
}
