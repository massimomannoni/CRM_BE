using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using Crm.Application.Employees;
using Crm.Application.Companies.Employees.GetCompanyEmployees;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompanyEmployeesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CompanyEmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// get company employees
        /// </summary>
        /// <param name="companyId"></param>
        [Route("{companyId}/employees")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeXDto>> GetCompanyEmployeesAsync([FromRoute] Guid companyId)
        {
            List<EmployeeDto> employees = await _mediator.Send(new GetCompanyEmployeesQuery(companyId));

            return Ok(employees);
        }

        /// <summary>
        /// add a new company employee
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/employees")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyEmployeeAsync([FromRoute] Guid companyId, [FromBody] RegisterCompanyEmployeeRequest request)
        {
            await _mediator.Send(new RegisterCompanyEmployeeCommand(companyId, request.Name, request.Surname, request.ContactType));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// remove a company employee
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeId"></param>
        [Route("{companyId}/employees/{employeId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyEmployeeAsync([FromRoute] Guid companyId, [FromRoute] Guid employeId)
        {
            await _mediator.Send(new RemoveCompanyEmployeeCommand(employeId, companyId));

            return Ok();
        }
    }
}
