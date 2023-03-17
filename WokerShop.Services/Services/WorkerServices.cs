using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Interfaces;

namespace WokerShop.Services.Services
{
    public class WorkerServices : IWorkerService
    {
        private readonly IMapper mapper;
        private readonly IWorkerRepository repository;

        public WorkerServices(IMapper mapper, IWorkerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task RegisterWorkerAsync(WorkerModel worker)
        {
            var workerDTO = mapper.Map<WorkerDTO>(worker);
            int lastDigits = int.Parse(workerDTO.Id.Substring(9, 3));
            if ((workerDTO.Sex.ToString().Equals("Male") && lastDigits > 500) || (workerDTO.Sex.ToString().Equals("Female") && lastDigits < 500))
                throw new ArgumentException("Not valid personalId");
           
            await repository.RegisterWorkerAsync(workerDTO);
        }
    }
}
