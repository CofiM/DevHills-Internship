using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Repository.DbContexts;
using WorkerShop.Repository.Entities;
using WorkerShop.Repository.Interfaces;

namespace WorkerShop.Repository.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly WorkerContext context;
        private readonly IMapper mapper;

        public ClientRepository(WorkerContext context, IMapper mapper)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CheckClientAsync(string id)
        {
            var client = await context.Clients.Where(p => p.Id == id).AsNoTracking().FirstOrDefaultAsync();
            bool check = true;
            if(client == null) 
            {
                check = false; 
            }
            return check;
        }

        public async Task<bool> CheckVehicleAsync(string vin)
        {
            bool check = true;
            var vehicle = await context.Vehicles.FindAsync(vin);
            if (vehicle == null)
            {
                //false znaci slobodno je vozilo
                check = false;
            }
            return check;
        }


        public async Task RegisterClientAsync(ClientDTO client)
        {
            var clientEntity = mapper.Map<Client>(client);
            var checkForClient = await CheckClientAsync(client.Id);
            //it tries to register already registered client
            if(checkForClient)
            {
                throw new ConflictException("Client already registered!");

            }
            //car already have owner so it's different owner or same
            //but because if already checked the owner we know that it's different owner
            bool checkForVehicle = true;
            foreach(var vehicle in clientEntity.Vehicles)
            {
                checkForVehicle = await CheckVehicleAsync(vehicle.VIN);
                if (checkForVehicle)
                    throw new ConflictException("Vehicle already registered with different owner!");
            }

            clientEntity.Created = DateTime.UtcNow;
            clientEntity.IsActive = true;
            context.Clients.Add(clientEntity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateClientAsync(ClientWithoutVehiclesDTO client)
        {
            Client clientEntity = mapper.Map<Client>(client);
            clientEntity.LastModifiedOn = DateTimeOffset.UtcNow;
            context.Clients.Update(clientEntity);
            await context.SaveChangesAsync();
        }

        public async Task PartiallyUpdateClientAsync(PatchClientDTO apiClient)
        {
            Client dbClient = mapper.Map<Client>(apiClient);
            dbClient.LastModifiedOn = DateTimeOffset.UtcNow;
            context.Clients.Update(dbClient);
            await context.SaveChangesAsync();
        }

        public async Task<Client> GetClientAsync(string id)
        {
            var client = await context.Clients.FindAsync(id);
            if(client == null)
            {
                throw new KeyNotFoundException("Client not in database");
            }
            return client;
        }

        public async Task RemoveClientAsync(string id)
        {
            var dbClient = await context.Clients.FindAsync(id);
            if(dbClient == null)
            {
                throw new BadRequestException("Client not in database");
            }
            //soft delete
            dbClient.IsActive = false;
            dbClient.DeletedOn = DateTimeOffset.UtcNow;
            await context.SaveChangesAsync();
        }

        public async Task<ClientPageDto> GetAllClients(int pageSize, int pageNumber, OrderByEnum order)
        {
            var clients = context.Clients.Include(c => c.Vehicles).AsQueryable();
            ClientPageDto pagination = new ClientPageDto();
            pagination.TotalRows = clients.Count();
            switch (order)
            {
                case OrderByEnum.FirstName:
                    clients = clients.OrderBy(item => item.FirstName);
                    break;

                case OrderByEnum.LastName:
                    clients = clients.OrderBy(item => item.LastName);
                    break;

                case OrderByEnum.CreateDate:
                    clients = clients.OrderBy(item => item.Created);
                    break;

                default:
                    clients = clients.OrderBy(item => item.FirstName);
                    break;
            }

            List<Client> paginatedClients = await clients.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            pagination.Clients = paginatedClients.Select(item => mapper.Map<ClientWithAdressDto>(item)).ToList();

            return pagination;
        }
    }
}
