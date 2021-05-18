using MediatR;
using Microsoft.AspNetCore.Mvc;
using Crm.Api.Services;
using Crm.Application.Companies;
using Crm.Application.Companies.RegisterCompany;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Application.Companies.GetCompanyDetails;
using Crm.Application.Companies.GetConpanies;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;
using System.Linq;
using Crm.Application.Companies.ChangeCompany;
using Crm.Application.Companies.GetCompaniesFiltered;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompaniesController : Controller
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get companies 
        /// </summary>
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<CompanyDetailsDto>>> GetCompaniesAsync()
        {
            var companies = await _mediator.Send(new GetCompaniesQuery());
            if (!companies.Any())    
                return NotFound();

            return Ok(companies);
        }

        /// <summary>
        /// Get company by id
        /// </summary>
        /// /// <param name="companyId"></param>
        [Route("{companyId}")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDetailsDto>> GetCompanyByIdAsync([FromRoute] Guid companyId)
        {
            var company = await _mediator.Send(new GetCompanyDetailsQuery(companyId));
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        /// <summary>
        /// Create a new company
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyDto>> RegisterCompanyAsync([FromBody] RegisterCompanyRequest request)
        {
           var company = await _mediator.Send(new RegisterCompanyCommand(request.Name, request.FiscalCode, request.PIva, request.Province, request.City, request.Address, request.Cap, request.ContractType, request.SubScriptionType, request.SubScriptionDate));

           return Created(string.Empty, company);
        }

        /// <summary>
        /// filter companies
        /// </summary>
        /// <param name="request"></param>
        [Route("filter")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyDto>> FilterCompanyAsync([FromBody] FilterCompanyRequest request)
        {
            var company = await _mediator.Send(new GetCompaniesFilteredQuery(request.Filter));
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        /// <summary>
        /// update a company by id
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeCompanyAsync([FromRoute]Guid companyId, [FromBody] RegisterCompanyRequest request)
        {
            await _mediator.Send(new ChangeCompanyCommand(companyId, request.Name, request.FiscalCode, request.PIva, request.Province, request.City, request.Address, request.Cap, request.ContractType, request.SubScriptionType, request.SubScriptionDate));

            return Ok();
        }

        /// <summary>
        /// delete a company by id
        /// </summary>
        /// <param name="companyId"></param>
        [Route("{companyId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveCompanyAsync([FromRoute] Guid companyId)
        {
            await _mediator.Send(new RemoveCompanyCommand(companyId));

            return Ok();
        }

    }
}
