using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using WokerShop.Services.Services;
using WorkerShop.API.Models;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;

namespace WorkerShop.API.Controllers
{
    [ApiController]
    [Route("api/client-management/individual")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;

        public ClientController(IClientService clientService, IMapper mapper)
        {
            this.clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(this.mapper));
        }

        /// <summary>
        /// Saves new client in database
        /// </summary>
        /// <param>
        /// Model with properties 
        /// </param>
        /// <returns>
        /// Return structure of client model
        /// </returns>
        ///<response code="201">Returns structure of client model that was added.</response>
        ///<response code="400">There is some error in arguments</response>
        ///<response code="409">Worker already exists in database</response>
        [HttpPost]
        [Route("RegisterClient")]
        public async Task<IActionResult> RegisterClientAsync([FromBody] ClientModel client)
        {
            try
            {
                await this.clientService.RegisterClientAsync(client);
                return Created("Created successfuly", client); ;
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }
        /// <summary>
        /// Creates or updates client depending if he is already in database.
        /// </summary>
        /// <param>
        /// Id of client and properties to be modified or added.
        /// </param>
        /// <returns>
        /// Return simple message.
        /// </returns>
        ///<response code="200">Client is updated.</response>
        ///<response code="201">Client is added.</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpPut]
        [Route("RegisterOrUpdateClient/{id}")]
        public async Task<IActionResult> RegisterOrUpdateClient([FromRoute] string id, [FromBody] ClientModel client)
        {
            try
            {
                var code = await this.clientService.CreateOrUpdateClient(id, client);
                if(code == StatusCodeEnum.Created)
                {
                    return Created("Succesfuly created.", null);
                }
                return Ok("Updated client");
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        ///Updates client depending if he is already in database.
        /// </summary>
        /// <param>
        /// Id of client and properties in json patch to be modified or added.
        /// </param>
        /// <returns>
        /// Return simple message.
        /// </returns>
        ///<response code="200">Client is updated.</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpPatch]
        [Route("PartiallyUpdateClient/{id}")]
        public async Task<IActionResult> PartiallyUpdateClient([FromRoute] string id, JsonPatchDocument<PatchClientDTO> client)
        {
            try
            {
                await this.clientService.PartiallyUpdateClient(id, client);
                return Ok();
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Soft deletes client in database.
        /// </summary>
        /// <param>
        /// Id of client to be deleted.
        /// ></param>
        /// <returns>
        /// Return message that it is successfuly soft deleted.
        /// </returns>
        ///<response code="201">Returns structure of client model that was added beforehand</response>
        ///<response code="400">There is some error in arguments</response>
        ///<response code="409">Worker already exists in database</response>
        [HttpDelete]
        [Route("RemoveClient/{id}")]
        public async Task<IActionResult> RemoveClient([FromRoute] string id)
        {
            try
            {
                var code = await this.clientService.RemoveClientAsync(id);
                return NoContent();
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Return asked client.
        /// </summary>
        /// <param>
        /// Id of client
        /// </param>
        /// <returns>
        /// Return client dto.
        /// </returns>
        ///<response code="200">Returns structure of client model that was added beforehand</response>
        ///<response code="404">There is not client in database with given id.</response>
        ///<response code="400">There is some error in arguments.</response>
        [HttpGet]
        [Route("GetClient/{id}")]
        public async Task<IActionResult> GetClient([FromRoute] string id)
        {
            try
            {
                var client = await this.clientService.GetClientAsync(id);
                return Ok(client);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// Returns paginated list of clients.
        /// </summary
        /// <returns>
        ///List of client defiend by number and size of the page
        /// </returns>
        ///<response code="200">Clien gets list of workers</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients([FromQuery] AllClientsDto request)
        {
            try
            {
                var client = await this.clientService.GetAllClients(request.PageSize, request.PageNumber, request.OrderBy);
                return Ok(client);
            }
            catch (BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
