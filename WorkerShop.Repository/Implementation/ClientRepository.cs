using AutoMapper;
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
            var client = await context.Clients.Where(p => p.Id == id).FirstOrDefaultAsync();
            bool check = true;
            if(client == null) { check = false; }
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

            if(checkForClient)
            {
                throw new ConflictException("Client already registered!");

            }

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

        public async Task UpdateClientAsync(ClientDTO client)
        {
            Client clientEntity = mapper.Map<Client>(client);
            clientEntity.LastModifiedOn = DateTimeOffset.UtcNow;
            context.Clients.Update(clientEntity);
            await context.SaveChangesAsync();
        }
    }
}
