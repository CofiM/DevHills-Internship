using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Models;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Entities;
using WorkerShop.Repository.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace WorkerShop.Repository.Implementation
{
    public class WorkOrderRepository : IWorkOrderRepository
    {
        private readonly WorkerContext context;
        private readonly IMapper mapper;

        public WorkOrderRepository(WorkerContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Worker> CheckAssignedWorkerAsync(string id)
        {
            var worker = await context.Workers.FindAsync(id);
            if(worker == null) 
            {
                throw new BadRequestException("Assigned worker is not registered.");
            }
            return worker;
        }

        public async Task<Vehicle> CheckVehicleAsync(string id)
        {
            var vehicle = await context.Vehicles.FindAsync(id);
            if(vehicle == null) 
            {
                throw new BadRequestException("Vehicle is not registered."); 
            }
            return vehicle;
        }

        public async Task CreateWorkOrderAsync(WorkOrderDto workOrder)
        {
            var dbworkeOrder = mapper.Map<WorkOrder>(workOrder);
            dbworkeOrder.Created = DateTime.UtcNow;
            dbworkeOrder.IsActive = true;

            if (!workOrder.AssignedWorkerId.IsNullOrEmpty()) 
            {
                var worker = await CheckAssignedWorkerAsync(workOrder.AssignedWorkerId);
                dbworkeOrder.Worker = worker;
            }
            var vehicle = await CheckVehicleAsync(workOrder.VIN);
            dbworkeOrder.Vehicle = vehicle;
            context.WorkOrders.Add(dbworkeOrder);
            await context.SaveChangesAsync();
        }

        public async Task DeleteWorkOrderAsync(Guid id)
        {
            var dbWorkOrder = await context.WorkOrders.FindAsync(id);
            if (dbWorkOrder == null)
                throw new KeyNotFoundException("Not in databse.");
            dbWorkOrder.IsActive = false;
            dbWorkOrder.DeletedOn = DateTimeOffset.UtcNow;
            context.WorkOrders.Update(dbWorkOrder);
            await context.SaveChangesAsync();   
        }

        public async Task<WorkOrderDto> GetWorkOrderAsync(Guid id)
        {
            var dbWorkOrder = await context.WorkOrders.FindAsync(id);
            if (dbWorkOrder == null)
                throw new KeyNotFoundException("Not in databse.");
            var dtoWorkOrder = mapper.Map<WorkOrderDto>(dbWorkOrder);
            return dtoWorkOrder;
        }

        public async Task UpdateWorkOrderAsync(Guid id, WorkOrderDto workOrder)
        {
            var dbWorkOrder = await context.WorkOrders.FindAsync(id);
            if (dbWorkOrder == null)
                throw new KeyNotFoundException("Not in database!");
            //dbWorkOrder = mapper.Map<WorkOrder>(workOrder); will overwrite the whole entity and create new object which will not be recongzide by database
            mapper.Map(workOrder, dbWorkOrder); //will apply changes but not create new object
            dbWorkOrder.LastModifiedOn = DateTimeOffset.UtcNow;
            context.WorkOrders.Update(dbWorkOrder);
            await context.SaveChangesAsync();
        }

        public async Task CompleteWorkOrderAsync(Guid id, string workerId, string notes)
        {
            var worker = await CheckAssignedWorkerAsync(workerId);
            var workOrder = await context.WorkOrders.FindAsync(id);
            if(workOrder == null)
            {
                throw new KeyNotFoundException("Work-order not in database.");
            }
            workOrder.AssignedWorkerId = worker.Id;
            workOrder.Worker = worker;
            workOrder.CompletedAt = DateTimeOffset.UtcNow;
            context.WorkOrders.Update(workOrder);
            await context.SaveChangesAsync();
        }

        public async Task<WorkOrderPage> SearchWorkOrdersAsync(SearchDto filters)
        {
            var query = context.WorkOrders.Include(opt => opt.Vehicle).Include(opt => opt.Worker).AsQueryable();
            WorkOrderPage pagination = new WorkOrderPage();
            if (!string.IsNullOrEmpty(filters.LicencePlate))
            {
                query = query.Where(w => w.Vehicle.LicensePlate == filters.LicencePlate);
            }
            if (!string.IsNullOrEmpty(filters.VIN))
            {
                query = query.Where(w => w.VIN == filters.VIN);
            }

            if (filters.Unassigned == true)
            {
                query = query.Where(w => w.Worker == null);
            }
            else
            {
                if (!string.IsNullOrEmpty(filters.WorkerId))
                {
                    query = query.Where(w => w.Worker.Id == filters.WorkerId);
                }
                if(filters.Status == CompleteEnum.Complete)
                {
                    query = query.Where(w => w.CompletedAt != null);
                }
                else
                {
                    query = query.Where(w => w.CompletedAt == null);
                }
            }
            query = query.OrderBy(opt => opt.Created);
            List<WorkOrder> workOrders = await query.Skip((filters.PageNumber-1) * filters.Size).Take(filters.Size).ToListAsync();
            pagination.WorkOrders = workOrders.Select(item => mapper.Map<WorkOrderPageDto>(item)).ToList();
            pagination.TotalRows = workOrders.Count;
            return pagination;
        }
    }
}
