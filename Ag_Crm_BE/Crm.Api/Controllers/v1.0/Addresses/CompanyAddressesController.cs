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
using Crm.Application.Addresses;
using Crm.Application.Addresses.GetCompanyAddresses;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompanyAddressesController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CompanyAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// get company addresses
        /// </summary>
        /// <param name="companyId"></param>
        [Route("{companyId}/addresses")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AddressesDto>> GetCompanyAddressesAsync([FromRoute] Guid companyId)
        {
            List<AddressesDto> addresses = await _mediator.Send(new GetCompanyAddressesQuery(companyId));

            return Ok(addresses);
        }

        /// <summary>
        /// add a new company address
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/addresses")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyAddressAsync([FromRoute] Guid companyId, [FromBody] RegisterCompanyAddressRequest request)
        {
            await _mediator.Send(new RegisterCompanyAddressCommand(companyId, request.AddressType, request.Value));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// remove a company address
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="addressId"></param>
        [Route("{companyId}/addresses/{addressId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyAddressAsync([FromRoute] Guid companyId, [FromRoute] Guid addressId)
        {
            await _mediator.Send(new RemoveAddressCommand(addressId, companyId));

            return Ok();
        }
    }
}
