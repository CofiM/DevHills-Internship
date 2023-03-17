using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Models;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Entities;
using WorkerShop.Repository.Interfaces;

namespace WorkerShop.Repository.Implementation
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly WorkerContext context;
        private readonly IMapper mapper;    

        public WorkerRepository(WorkerContext context,IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> ExistsWorkerAsync(string id)
        {
            var worker = await context.Workers.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (worker == null)
                return false;
            else
                return true;
        }

        public async Task RegisterWorkerAsync(WorkerDTO worker)
        {
            var workerEntity = mapper.Map<Worker>(worker);
            var check = await ExistsWorkerAsync(workerEntity.Id);
            if (check)
                throw new Exception("Worker already exists!");
            context.Workers.Add(workerEntity);
            await context.SaveChangesAsync();
            
        }
    }
}
