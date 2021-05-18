
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Dimensions.GetDimensions
{
    public class GetDimensionTypesQuery : IRequest<List<DimensionTypesDto>>
    {
        public GetDimensionTypesQuery()
        {
          
        }
    }
}
