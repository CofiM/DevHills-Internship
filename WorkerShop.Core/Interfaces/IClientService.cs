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
    public interface IClientService
    {
        Task RegisterClientAsync(ClientModel client);

        Task<StatusCodeEnum> CreateOrUpdateClient(string id, ClientDTO client);
    }
}
