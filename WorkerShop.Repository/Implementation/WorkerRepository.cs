using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Models;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Entities;
using WorkerShop.Repository.Interfaces;

namespace WorkerShop.Repository.Implementation
{
    public class WorkerRepository : IWokOrderRepository
    {
        private readonly WorkerContext context;
        private readonly IMapper mapper;    

        public WorkerRepository(WorkerContext context,IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task DeleteWorkerAsync(string id)
        {
            var worker = await context.Workers.Where(p => p.Id == id && p.IsActive == true).FirstOrDefaultAsync();
            if (worker == null)
            {
                throw new BadRequestException("Worker doesn't exists!");
            }
            worker.IsActive = false;
            worker.DeletedOn = DateTimeOffset.UtcNow;
            await context.SaveChangesAsync();
        }

        public async Task<bool> ExistsWorkerAsync(string id)
        {
            var worker = await context.Workers.Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
            if (worker == null)
                return false;
            else
                return true;
        }

        public async Task<List<Worker>> GetAllWorkersAsync()
        {
            return await context.Workers.ToListAsync();
        }

        public async Task<Worker> GetWorkerAsync(string id)
        {
            return await context.Workers.Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task PartiallyUpdateWorker(PatchWorkerDto workerPatch, string id)
        {
            var worker = await context.Workers.Where(p => p.Id == id).FirstOrDefaultAsync();
            mapper.Map(workerPatch,worker);
            await context.SaveChangesAsync();
        }

        public async Task RegisterWorkerAsync(WorkerDTO worker)
        {
            var workerEntity = mapper.Map<Worker>(worker);
            var check = await ExistsWorkerAsync(workerEntity.Id);
            if (check)
            {
                throw new ConflictException("Worker already exists!");
            }
            workerEntity.Created = DateTime.UtcNow;
            workerEntity.IsActive = true;
            //how to make it work with constructor with args, because it returns default values when using constructor with args.Check worker entity constructor and 67-68 lines.
            context.Workers.Add(workerEntity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateWorkerAsync(string id, WorkerDTO workerDto)
        {
            //had problem for tracking, used AsNoTracking, maybe good topic for discussion  
            Worker workerMapped = mapper.Map<Worker>(workerDto);
            workerMapped.LastModifiedOn = DateTimeOffset.UtcNow;
            context.Workers.Update(workerMapped);
            await context.SaveChangesAsync();
        }

    }
}
