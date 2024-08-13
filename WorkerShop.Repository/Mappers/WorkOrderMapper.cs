using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.Mappers
{
    public class WorkOrderMapper : Profile
    {
        public WorkOrderMapper() 
        {
            CreateMap<WorkOrderModel, WorkOrderDto>();
            CreateMap<WorkOrderDto, WorkOrderModel>();

            CreateMap<WorkOrderDto, WorkOrder>();
            CreateMap<WorkOrder, WorkOrderDto>();

            CreateMap<SearchRequest, SearchDto>();

            CreateMap<WorkOrder, WorkOrderPageDto>()
                .ForMember(dest => dest.WorkerName, opt => opt.MapFrom(src => $"{src.Worker.FirstName} {src.Worker.LastName}"))
                .ForMember(dest => dest.LicensePlate, opt => opt.MapFrom(src => src.Vehicle.LicensePlate))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Created != null ? CompleteEnum.Complete : CompleteEnum.New));
        }
    }
}
//public string VIN { get; set; } ima 
//public string? WorkerName { get; set; } nema
//public string? LicensePlate { get; set; } nema
//public CompleteEnum Status { get; set; } nema
//public DateTime Created { get; set; } ima
//public DateTimeOffset CompletedAt { get; set; } ima