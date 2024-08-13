using AutoMapper;
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
    public class VehicleMapper : Profile
    {
        public VehicleMapper() 
        {
            CreateMap<VehicleModel, VehicleDTO>()
                .ForMember(dest => dest.Power, temp =>
                temp.MapFrom(src => src.PowerEnum == Core.Enums.PowerEnum.BHP ? src.Power/1.341 : src.Power));
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<Vehicle, VehicleDTO>();
        }
    }
}
