using AutoMapper;
using Azure.Core.Pipeline;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Entities;
using WorkerShop.Repository.Interfaces;

namespace WokerShop.Services.Services
{
    public class ClientService : IClientService
    {

        private readonly IMapper mapper;
        private readonly IClientRepository repository;

        public ClientService(IMapper mapper, IClientRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<StatusCodeEnum> CreateOrUpdateClient(string id, ClientModel client)
        {
            if (client.Id != id)
            {
                throw new BadRequestException("Not matching ids!");
            }
            int lastDigits = int.Parse(client.Id.Substring(9, 3));
            if ((client.Sex == SexEnum.Male && lastDigits > 500) || (client.Sex == SexEnum.Female && lastDigits < 500))
            {
                throw new BadRequestException("Not valid personalId");
            }

            bool exisitingClient = await repository.CheckClientAsync(id);

            if (exisitingClient == true)
            {
                if (client.Vehicles.Any() == true)
                {
                    throw new BadRequestException("Not allowed to update vehicles");
                }
                var updatedClient = mapper.Map<ClientWithoutVehiclesDTO>(client);
                await repository.UpdateClientAsync(updatedClient);
                return StatusCodeEnum.Ok;
            }

            var createdClient = mapper.Map<ClientDTO>(client);
            await repository.RegisterClientAsync(createdClient);
            return StatusCodeEnum.Created;

        }


        public async Task<StatusCodeEnum> RegisterClientAsync([FromBody] ClientModel client)
        {
            var clientDTO = mapper.Map<ClientDTO>(client);
            int lastDigits = int.Parse(clientDTO.Id.Substring(9, 3));

            if ((clientDTO.Sex == SexEnum.Male && lastDigits > 500) || (clientDTO.Sex == SexEnum.Female && lastDigits < 500))
            {
                throw new BadRequestException("Not valid personalId");
            }

            await repository.RegisterClientAsync(clientDTO);
            return StatusCodeEnum.Ok;
        }

        public async Task<StatusCodeEnum> PartiallyUpdateClient(string id, JsonPatchDocument<PatchClientDTO> patch)
        {

            var dataModelClient = await repository.GetClientAsync(id);
            if (dataModelClient == null)
            {
                throw new KeyNotFoundException("Client not in database");
            }
            var apiModelClient = mapper.Map<PatchClientDTO>(dataModelClient);
            patch.ApplyTo(apiModelClient);
            if (apiModelClient.Id != dataModelClient.Id)
            {
                throw new BadRequestException("Modifying the keys is not allowed");
            }
            //we have to check if vehicles are modified to notify that's invalid action(thats task)
            //but for values we can just ignore property in mapper
            foreach (var operation in patch.Operations)
            {
                if (operation.path.StartsWith("/Vehicles", StringComparison.OrdinalIgnoreCase))
                {
                    throw new BadRequestException("Modifying the vehicles is not allowed.");
                }
            }
            await repository.PartiallyUpdateClientAsync(apiModelClient);
            return StatusCodeEnum.Ok;
        }

        public async Task<StatusCodeEnum> RemoveClientAsync(string id)
        {
            await repository.RemoveClientAsync(id);
            return StatusCodeEnum.NoContent;
        }

        public async Task<ClientWithAdressDto> GetClientAsync(string id)
        {
            var dbClient = await repository.GetClientAsync(id);
            var apiClient = mapper.Map<ClientWithAdressDto>(dbClient);
            return apiClient;
        }

        public async Task<ClientPageDto> GetAllClients(int pageSize, int pageNumber, OrderByEnum order)
        {
            ClientPageDto page = await repository.GetAllClients(pageSize, pageNumber, order);
            return page;
        }
    }
}
