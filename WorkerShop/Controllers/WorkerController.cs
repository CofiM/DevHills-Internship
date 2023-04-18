using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkerShop.API.Models;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;

namespace WorkerShop.API.Controllers
{
    [ApiController]
    [Route("api/workers/")]
    public class WorkerController : ControllerBase
    {
       private readonly IWorkerService _workerService;
       private readonly IMapper mapper;

       public WorkerController(IWorkerService workerService, IMapper mapper)
        {
            _workerService = workerService ?? throw new ArgumentNullException(nameof(_workerService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(this.mapper));
        }

       [HttpPost]
       [Route("RegisterWorker")]
        public async Task<IActionResult> RegisterWorkerAsync([FromBody]WorkerModel worker)
       {
            try
            {
                await _workerService.RegisterWorkerAsync(worker);
                return Created("Created successfuly", worker);
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
            catch(ConflictException ex)
            {
                return Conflict(ex.Message);
            }
       }

        [HttpDelete]
        [Route("DeleteWorker/{id}")]
        public async Task<IActionResult> DeleteWorkerAsync([FromRoute]string id)
        {
            try
            {
                await _workerService.UnregisterWorkerAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
