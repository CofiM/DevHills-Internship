using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.Interfaces
{
    public interface IWorkOrderRepository
    {
        public Task CreateWorkOrderAsync(WorkOrderDto workOrder);
        public Task<Worker> CheckAssignedWorkerAsync(string id);
        public Task<Vehicle> CheckVehicleAsync(string id);

        public Task<WorkOrderDto> GetWorkOrderAsync(Guid id);

        public Task DeleteWorkOrderAsync(Guid id);

        public Task UpdateWorkOrderAsync(Guid id, WorkOrderDto workOrder);

        public Task CompleteWorkOrderAsync(Guid id, string workedId, string notes);

        public Task<WorkOrderPage> SearchWorkOrdersAsync(SearchDto filters);
    }
}
