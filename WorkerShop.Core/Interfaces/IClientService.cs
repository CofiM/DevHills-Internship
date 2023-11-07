using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.Models;

namespace WorkerShop.Core.Interfaces
{
    public interface IClientService
    {
        Task RegisterClientAsync(ClientModel client);
    }
}
