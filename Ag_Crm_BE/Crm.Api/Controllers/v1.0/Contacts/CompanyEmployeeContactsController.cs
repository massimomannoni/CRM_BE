using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Crm.Application.Employees;
using Crm.Application.Contacts.GetCompanyEmployeeContacts;
using Crm.Application.Contacts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompanyEmployeeContactsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CompanyEmployeeContactsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// get company employee contacts
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        [Route("{companyId}/employees/{employeeId}/contacts")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContactDto>> GetCompanyEmployeeContactsAsync([FromRoute] Guid companyId, [FromRoute] Guid employeeId)
        {
            List<ContactDto> employees = await _mediator.Send(new GetCompanyEmployeeContactsQuery(companyId, employeeId));

            return Ok(employees);
        }

        /// <summary>
        /// add a new company employee contact
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/employees/{employeeId}/contacts")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyEmployeeContactAsync([FromRoute] Guid companyId, [FromRoute] Guid employeeId, [FromBody] RegisterEmployeeContactRequest request)
        {
            await _mediator.Send(new RegisterEmployeeContactCommand(companyId, employeeId, request.AddressType, request.Value));

            return Created(string.Empty, null);

        }

        /// <summary>
        /// remove a company employee contact
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="contactId"></param>
        [Route("{companyId}/employees/{employeeId}/contacts/{contactId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyEmployeeAsync([FromRoute] Guid companyId, [FromRoute] Guid employeeId, [FromRoute] Guid contactId)
        {
            await _mediator.Send(new RemoveEmployeeContactCommand(companyId, employeeId, contactId));

            return Ok();
        }
    }
}
