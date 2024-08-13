using Microsoft.AspNetCore.JsonPatch;
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
        Task<StatusCodeEnum> RegisterClientAsync(ClientModel client);

        Task<StatusCodeEnum> CreateOrUpdateClient(string id, ClientModel client);

        Task<StatusCodeEnum> PartiallyUpdateClient(string id, JsonPatchDocument<PatchClientDTO> client);

        Task<StatusCodeEnum> RemoveClientAsync(string id);

        Task<ClientWithAdressDto> GetClientAsync(string id);

        Task<ClientPageDto> GetAllClients(int pageSize, int pageNumber, OrderByEnum order);
    }
}
