using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Models;

namespace WorkerShop.Core.Interfaces
{
    public interface IWorkOrderService
    {
        Task<StatusCodeEnum> CreateWorkOrderAsync(WorkOrderModel workOrder);
        Task DeleteWorkOrderAsync(Guid id);
        Task<StatusCodeEnum> CreateOrUpdateWorkOrderAsync(CreateOrUpdateWorkOrderModel model);
        Task CompleteWorkOrderAsync(Guid id, string workerId, string notes);
        Task<WorkOrderPage> SearchWorkOrdersAsync(SearchRequest searchRequest);
    }
}
