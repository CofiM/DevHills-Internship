using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Models;

namespace WorkerShop.Core.Interfaces
{
    public interface IWorkerService
    {
        Task RegisterWorkerAsync(WorkerModel worker); 
    }
}
