
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Activities.GetActivities
{
    public class GetActivityTypesQuery : IRequest<List<ActivityTypesDto>>
    {
        public GetActivityTypesQuery()
        {
          
        }
    }
}
