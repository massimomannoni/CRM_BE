using Crm.Api.Services;
using Crm.Application.Addresses;
using Crm.Application.Addresses.GetAddresses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.AddressTypes)]
    [ApiController]
    public class AddressTypesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AddressTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get address types
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<AddressTypesDto>>> GetAddresses()
        {
            var addressTypes = await _mediator.Send(new GetAddressTypesQuery());
            if (!addressTypes.Any())
                return NotFound();

            return Ok(addressTypes);
        }
       
    }
}
