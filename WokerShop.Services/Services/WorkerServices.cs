﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerShop.API.Models;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;
using WorkerShop.Repository.Interfaces;

namespace WokerShop.Services.Services
{
    public class WorkerServices : IWorkerService
    {
        private readonly IMapper mapper;
        private readonly IWorkerRepository repository;

        public WorkerServices(IMapper mapper, IWorkerRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }
        public async Task RegisterWorkerAsync(WorkerModel worker)
        {
            var workerDTO = mapper.Map<WorkerDTO>(worker);
            int lastDigits = int.Parse(workerDTO.Id.Substring(9, 3));

            if ((workerDTO.Sex.ToString().Equals("Male") && lastDigits > 500) || (workerDTO.Sex.ToString().Equals("Female") && lastDigits < 500))
            {
                throw new BadRequestException("Not valid personalId");
            }

            string year = workerDTO.Id.Substring(3, 3);
            //0 means its in 2000s so we have to check for years,otherwise its 1900s and we dont have to check
            if (year[0] == '0')
            {
                int fullYear = int.Parse('2' + year);
                int day = int.Parse(workerDTO.Id.Substring(0, 2));
                int month = int.Parse(workerDTO.Id.Substring(2, 2));
                
                DateTime date = new DateTime(fullYear, month, day);

                //if worker date is later then limit date, it means that person is not yet 18. 
                if (DateTime.Today.AddYears(-18) < date)
                {
                    throw new BadRequestException("Underaged.");
                }
            }
            await repository.RegisterWorkerAsync(workerDTO);
        }
    }
}
