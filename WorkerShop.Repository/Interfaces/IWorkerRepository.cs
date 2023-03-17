using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.Interfaces
{
    public interface IWorkerRepository
    {
        Task<bool> ExistsWorkerAsync(string id);

        Task RegisterWorkerAsync(WorkerDTO worker);
    }
}
