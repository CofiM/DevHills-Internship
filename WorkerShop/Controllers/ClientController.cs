using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WokerShop.Services.Services;
using WorkerShop.API.Models;
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
    }
}
