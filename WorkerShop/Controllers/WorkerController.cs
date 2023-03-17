using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkerShop.API.Models;
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
       public async Task<IActionResult> RegisterWorkerAsync(WorkerModel worker)
       {
            try
            {
                await _workerService.RegisterWorkerAsync(worker);
                return Ok();
            }
            catch(Exception ex)
            {

            }
       }
    }
}
