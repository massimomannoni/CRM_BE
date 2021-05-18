using Crm.Api.Services;
using Crm.Application.Activities;
using Crm.Application.Activities.GetCompanyActivities;
using Crm.Application.Addresses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Crm.Api.Controllers
{
    [Route(ApiBase.Companies)]
    [ApiController]
    public class CompanyActivitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get company activites 
        /// </summary>
        [Route("{companyId}/activities")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ActivityDto>>>GetActivities(Guid companyId)
        {
            var activities = await _mediator.Send(new GetCompanyActivitiesQuery(companyId));

            if (!activities.Any())
                return NotFound();

            return Ok(activities);
        }

        /// <summary>
        /// add a new company activity
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="request"></param>
        [Route("{companyId}/activities")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> AddCompanyActivityAsync([FromRoute] Guid companyId, [FromBody] RegisterCompantActivityRequest request)
        {
            await _mediator.Send(new RegisterCompanyActivityCommand(companyId, request.ActivityType, request.SectorType, request.Value));

            return Created(string.Empty, null);
        }

        /// <summary>
        /// remove a company activity
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="activityId"></param>
        [Route("{companyId}/activities/{activityId}")]
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> DelCompanyActivityAsync([FromRoute] Guid companyId, [FromRoute] Guid activityId)
        {
            await _mediator.Send(new RemoveCompanyActivityCommand(companyId, activityId));

            return Ok();
        }

    }
}