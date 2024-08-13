using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Interfaces;

namespace WokerShop.Services.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IMapper mapper;
        private readonly IWorkOrderRepository repository;

        public WorkOrderService(IMapper mapper, IWorkOrderRepository repository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<StatusCodeEnum> CreateWorkOrderAsync(WorkOrderModel workOrder)
        {
            var workOrderDto = mapper.Map<WorkOrderDto>(workOrder);
            await repository.CreateWorkOrderAsync(workOrderDto);
            return StatusCodeEnum.Created;
        }

        public async Task DeleteWorkOrderAsync(Guid id)
        {
            await repository.DeleteWorkOrderAsync(id);
        }

        public async Task<StatusCodeEnum> CreateOrUpdateWorkOrderAsync(CreateOrUpdateWorkOrderModel model)
        {
            StatusCodeEnum status = StatusCodeEnum.Created;
            var dtoWorkOrder = mapper.Map<WorkOrderDto>(model.workOrder);
            if (model.Id == null)
            {
                await repository.CreateWorkOrderAsync(dtoWorkOrder);
            }
            else
            {
                await repository.UpdateWorkOrderAsync((Guid)model.Id, dtoWorkOrder);
                status = StatusCodeEnum.Ok;
            }
            return status;
        }

        public async Task CompleteWorkOrderAsync(Guid id, string workerId, string notes)
        {
            await repository.CompleteWorkOrderAsync(id, workerId, notes);
        }

        public async Task<WorkOrderPage> SearchWorkOrdersAsync(SearchRequest searchRequest)
        {
            var filters = mapper.Map<SearchDto>(searchRequest);
            return await repository.SearchWorkOrdersAsync(filters);
        }
    }
}
