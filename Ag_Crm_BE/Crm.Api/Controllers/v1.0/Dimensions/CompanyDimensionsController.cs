using Crm.Api.Services;
using Crm.Application.Dimensions;
using Crm.Application.Dimensions.GetCompanyDimensions;
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
    public class CompanyDimensionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyDimensionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get company dimensions 
        /// </summary>
        [Route("{companyId}/dimensions")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<DimensionDto>>>GetDimensions(Guid companyId)
        {
            var dimensions = await _mediator.Send(new GetCompanyDimensionsQuery(companyId));

            return Ok(dimensions);
        }

        /// <summary>
        /// add a new company dimensions
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/dimensions")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyDimensionAsync([FromRoute] Guid companyId, [FromBody] RegisterCompanyDimensionRequest request)
        {
            await _mediator.Send(new RegisterCompanyDimensionCommand(companyId, request.DimensionType, request.Fee));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// remove a company dimensions
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="dimensionId"></param>
        [Route("{companyId}/dimensions/{dimensionId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyDimensionAsync([FromRoute] Guid companyId, [FromRoute] Guid dimensionId)
        {
            await _mediator.Send(new RemoveCompanyDimensionCommand(dimensionId, companyId));

            return Ok();
        }
    }
}