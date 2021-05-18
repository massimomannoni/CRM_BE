using Crm.Api.Services;
using Crm.Application.Activities;
using Crm.Application.Activities.GetActivities;
using Crm.Application.Activities.GetSectors;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.SectorTypes)]
    [ApiController]
    public class SectorTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SectorTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get activity types 
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ActivityTypesDto>>> GetAddresses()
        {
            var addressTypes = await _mediator.Send(new GetSectorTypesQuery());
            if (!addressTypes.Any())
                return NotFound();

            return Ok(addressTypes);
        }
       
    }
}
