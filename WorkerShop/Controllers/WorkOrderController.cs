using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using WorkerShop.Core.Enums;
using WorkerShop.Core.Exceptions;
using WorkerShop.Core.Interfaces;
using WorkerShop.Core.Models;

namespace WorkerShop.API.Controllers
{
    [ApiController]
    [Route("api/work-order/")]
    public class WorkOrderController : Controller
    {
        private readonly IWorkOrderService _workOrderService;
        private readonly IMapper _mapper;

        public WorkOrderController(IWorkOrderService workOrderService, IMapper mapper)
        {
            _workOrderService= workOrderService ?? throw new ArgumentNullException(nameof(_workOrderService));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(this._mapper));
        }
        /// <summary>
        /// Create new work order.
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns>
        /// Simple string message that everything is ok.
        /// </returns>
        /// <response code="201"> The work order is successfully created</response>
        /// <response code="400"> The input model is invalid or either worker or vehicle is not in database or</response>
        [HttpPost]
        [Route("CreateWorkOrder")]
        [ProducesResponseType(typeof(WorkOrderModel),201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateWorkOrderAsync([FromBody] WorkOrderModel workOrder)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var status = await _workOrderService.CreateWorkOrderAsync(workOrder);
                return Created("Work-order created.", null);
            }
            catch(BadRequestException e)
            {
                return BadRequest(e.Message);
            }
        }

        ///<summary>
        ///Delete existing work order in database
        ///</summary>
        ///<param>
        ///Id of work order
        ///</param>
        /// <returns>
        /// Simple string message that everything is ok.
        /// </returns>
        /// <response code="204"> The work order is successfully deleted</response>
        /// <response code="404"> The input id of work order is not in database or</response>
        /// <response code="400"> Catching other exceptions</response>
        [HttpDelete]
        [Route("DeleteWorkOrder/{id}")]
        [ProducesResponseType(204), ProducesResponseType(404), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteWorkOrder([FromRoute] Guid id)
        {
            try
            {
                await _workOrderService.DeleteWorkOrderAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        ///<summary>
        ///Creates or updates work order depending on the input id. If id is null then create, otherwise update
        ///</summary>
        ///<param>
        ///Nullable id and model of work order
        ///</param>
        /// <returns>
        /// Simple string message that everything is ok.
        /// </returns>
        /// <response code="200"> The work order is successfully updated</response>
        /// <response code="201"> The work order is successfully created</response>
        /// <response code="404"> The input id of work order is not in database or</response>
        /// <response code="400"> Catching other exceptions</response>
        [HttpPut]
        [Route("CreateOrUpdateWorkOrder")]
        [ProducesResponseType(200), ProducesResponseType(201), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> CreateOrUpdateWorkOrder([FromBody] CreateOrUpdateWorkOrderModel model)
        {
            try
            {
                var status = await _workOrderService.CreateOrUpdateWorkOrderAsync(model);
                if (status == StatusCodeEnum.Ok)
                {
                    return Ok("Work order is updated.");
                }
                return Created("Created new work order.", null);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        ///<summary>
        ///Complete the work-order by adding worker and date of completion.
        ///</summary>
        ///<param>
        ///Id of work-order to be modified and worker's id who completed.
        ///</param>
        /// <returns>
        /// Simple string message that everything is ok.
        /// </returns>
        /// <response code="200"> The work order is successfully completed</response>
        /// <response code="400"> Catching other exceptions</response>
        [HttpPut]
        [Route("CompleteWorkOrder")]
        public async Task<IActionResult> CompleteWorkOrder([FromBody] CompleteWorkOrderRequest request)
        {
            try
            {
                await _workOrderService.CompleteWorkOrderAsync(request.Id, request.WorkerId, request.Notes);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ///<summary>
        ///Return page of work-order by given filters and size
        ///</summary>
        ///<param>
        ///Page,size and filter of VIN,LicencePlates,Completion
        ///</param>
        /// <returns>
        /// List of filtered work order.
        /// </returns>
        /// <response code="200"> Everything is okay </response>
        /// <response code="400"> Catching other exceptions</response>
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromBody] SearchRequest searchRequest)
        {
            try
            {
                var result = await _workOrderService.SearchWorkOrdersAsync(searchRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
