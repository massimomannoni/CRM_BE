using Crm.Api.Services;
using Crm.Application.EmployeesOverViews;
using Crm.Application.EmployeesOverViews.GetCompanyEmployeesOverView;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompanyEmployeesOverViewController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyEmployeesOverViewController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get company employeesOverViews
        /// </summary>
        [Route("{companyId}/employeesOverViews")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<EmployeesOverViewDto>>> GetGetCompanyEmployeeOvers(Guid companyId)
        {
            var employeesOverViews = await _mediator.Send(new GetCompanyEmployeeOverViewQuery(companyId));

            return Ok(employeesOverViews);
        }

        /// <summary>
        /// add a new company employeesOverViews
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/employeesOverViews")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyEmployeesOverViewAsync([FromRoute] Guid companyId, [FromBody] RegisterCompanyEmployeesOverViewRequest request)
        {
            await _mediator.Send(new RegisterCompanyEmployeesOverViewCommand(companyId, request.ContractLevelType, request.Employees));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// remove a company employeesOverViews
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeesOverViewId"></param>
        [Route("{companyId}/employeesOverViews/{employeesOverViewId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyEmployeesOverViewAsync([FromRoute] Guid companyId, [FromRoute] Guid employeesOverViewId)
        {
            await _mediator.Send(new RemoveCompanyEmployeesOverViewCommand(employeesOverViewId, companyId));

            return Ok();
        }

    }
}