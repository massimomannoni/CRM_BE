using Crm.Api.Services;
using Crm.Application.Contacts;
using Crm.Application.Contacts.GetContactTypes;
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
    [Route(ApiBase.ContactTypes)]
    [ApiController]

    public class ContactsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get contact types 
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ContactTypesDto>>> GetContactTypes()
        {
            var contactTypes = await _mediator.Send(new GetContactTypesQuery());
            if (!contactTypes.Any())
                return NotFound();

            return Ok(contactTypes);
        }
    }
}
