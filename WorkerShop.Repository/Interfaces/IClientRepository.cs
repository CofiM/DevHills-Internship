using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;

namespace WorkerShop.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task RegisterClientAsync(ClientDTO client);

        Task<bool> CheckClientAsync(string id);
    }
}
