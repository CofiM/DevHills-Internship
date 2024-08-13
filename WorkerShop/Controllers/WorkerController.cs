using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WorkerShop.API.Models;
using WorkerShop.Core.DTOs;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;

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
        /// <summary>
        /// Saves new worker in database
        /// </summary>
        /// <param name="worker"></param>
        /// <returns>
        /// Return structure of worker model
        /// </returns>
        ///<response code="201">Returns structure of worker model that was added beforehand</response>
        ///<response code="400">There is some error in arguments</response>
        ///<response code="409">Worker already exists in database</response>
        [HttpPost]
        [Route("RegisterWorker")]
        [ProducesResponseType(typeof(WorkerModel),201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> RegisterWorkerAsync([FromBody] WorkerModel worker)
        {
            try
            {
                await _workerService.RegisterWorkerAsync(worker);
                return Created("Created successfuly", worker);
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
        /// Soft deletes worker in database.
        /// </summary>
        /// <param>
        /// Id of worker to be deleted.
        /// ></param>
        /// <returns>
        /// Return message that it is successfuly soft deleted.
        /// </returns>
        ///<response code="201">Returns structure of worker model that was added beforehand</response>
        ///<response code="400">There is some error in arguments</response>
        ///<response code="409">Worker already exists in database</response>
        [HttpDelete]
        [Route("DeleteWorker/{id}")]
        public async Task<IActionResult> DeleteWorkerAsync([FromRoute] string id)
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

        /// <summary>
        /// Return asked worker.
        /// </summary>
        /// <param>
        /// Id of worker
        /// </param>
        /// <returns>
        /// Return worked dto.
        /// </returns>
        ///<response code="200">Returns structure of worker model that was added beforehand</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpGet]
        [Route("GetWorker/{id}")]
        public async Task<IActionResult> GetWorkerAsync([FromRoute] string id)
        {
            try
            {
                var worker = await _workerService.GetWorkerAsync(id);
                return Ok(worker);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Returns all workers.
        /// </summary
        /// <returns>
        /// List of all workers.
        /// </returns>
        ///<response code="200">Client gets list of workers</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpGet]
        [Route("GetWorkerList")]
        public async Task<IActionResult> GetWorkerListAsync()
        {
            try
            {
                var workerList = await _workerService.GetWorkerListAsync();
                return Ok(workerList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates or updates worker depending if he is already in database.
        /// </summary>
        /// <param>
        /// Id of worker and properties to be modified or added.
        /// </param>
        /// <returns>
        /// Return simple message.
        /// </returns>
        ///<response code="200">Worker is updated.</response>
        ///<response code="201">Worker is added.</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpPut]
        [Route("CreateOrUpdateWorker/{id}")]
        public async Task<IActionResult> CreateOrUpdateWorker([FromRoute] string id, [FromBody] WorkerModel worker)
        {
            try
            {
                var workerDto = mapper.Map<WorkerDTO>(worker);
                var status = await _workerService.CreateOrUpdateWorker(id, workerDto);
                if(status == StatusCodeEnum.Created)
                {
                    return Created("Succesfuly created.", null);
                }
                return Ok();

            }
            catch (ConflictException e)
            {
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        ///Updates worker depending if he is already in database.
        /// </summary>
        /// <param>
        /// Id of worker and properties in json patch to be modified or added.
        /// </param>
        /// <returns>
        /// Return simple message.
        /// </returns>
        ///<response code="200">Worker is updated.</response>
        ///<response code="400">There is some error in arguments</response>
        [HttpPatch]
        [Route("PartiallyUpdateWorker/{id}")]
        public async Task<IActionResult> PartiallyUpdateWorker([FromRoute] string id, JsonPatchDocument<PatchWorkerDto> patch)
        {
            try
            {
                var worker = await _workerService.GetWorkerAsync(id);
                if (worker == null)
                {
                    throw new BadRequestException("Workere does not exist");
                }
                var workerPatch = mapper.Map<PatchWorkerDto>(worker); 
                patch.ApplyTo(workerPatch,ModelState);
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if(!TryValidateModel(workerPatch))
                {
                    return BadRequest(ModelState);
                }
                await _workerService.PatchWorkerAsync(workerPatch, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
