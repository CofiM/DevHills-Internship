using AutoMapper;
using WorkerShop.API.Models;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Entities;

namespace WorkerShop.API.Mappers
{
    public class WorkerMapper : Profile
    {

        public WorkerMapper() 
        {
            CreateMap<WorkerModel,WorkerDTO>();
            CreateMap<WorkerDTO, Worker>();
            CreateMap<Worker, WorkerDTO>();
            CreateMap<WorkerDTO, WorkerModel>();
            CreateMap<Worker, WorkerWithFullNameDto>();
        }
    }
}
