using AutoMapper;
using Azure.Core.Pipeline;
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

        public async Task RegisterClientAsync(ClientModel client)
        {
            var clientDTO = mapper.Map<ClientDTO>(client);
            int lastDigits = int.Parse(clientDTO.Id.Substring(9, 3));

            if ((clientDTO.Sex == SexEnum.Male && lastDigits > 500) || (clientDTO.Sex == SexEnum.Female && lastDigits < 500))
            {
                throw new BadRequestException("Not valid personalId");
            }

            await repository.RegisterClientAsync(clientDTO);
        }
    }
}
