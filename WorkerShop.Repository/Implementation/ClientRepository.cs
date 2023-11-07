using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task RegisterClientAsync(ClientDTO client)
        {
            var clientEntity = mapper.Map<Client>(client);
            var check = await CheckClientAsync(client.Id);
            if(check)
            {
                throw new ConflictException("Client already registered!");

            }
            clientEntity.Created = DateTime.UtcNow;
            clientEntity.IsActive = true;
            context.Clients.Add(clientEntity);
            await context.SaveChangesAsync();
        }
    }
}
