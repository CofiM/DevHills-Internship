using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Models;

namespace WorkerShop.Core.Interfaces
{
    public interface IWorkerService
    {
        Task RegisterWorkerAsync(WorkerModel worker);

        Task UnregisterWorkerAsync(string id);

        bool ValidateWorker(WorkerDTO worker);

        bool ValidatePersonalId(string id);

        Task<WorkerWithFullNameDto> GetWorkerAsync(string id);

        Task<List<WorkerWithFullNameDto>> GetWorkerListAsync();

        Task<StatusCodeEnum> CreateOrUpdateWorker(string id, WorkerDTO worker);

        Task PatchWorkerAsync(PatchWorkerDto workerPatch, string id);
    }
}
