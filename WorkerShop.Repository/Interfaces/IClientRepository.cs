using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Repository.Entities;

namespace WorkerShop.Repository.Interfaces
{
    public interface IClientRepository
    {
        Task RegisterClientAsync(ClientDTO client);

        Task<bool> CheckClientAsync(string id);

        Task<bool> CheckVehicleAsync(string id);

        Task UpdateClientAsync(ClientWithoutVehiclesDTO client);

        Task PartiallyUpdateClientAsync(PatchClientDTO apiClient);

        Task<Client> GetClientAsync(string id);

        Task RemoveClientAsync (string id);

        Task<ClientPageDto> GetAllClients(int pageSize, int pageNumber, OrderByEnum order);


    }
}
