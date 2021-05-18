
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Activities.GetCompanyActivities
{
    public class GetCompanyActivitiesQuery : IRequest<List<ActivityDto>>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyActivitiesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
