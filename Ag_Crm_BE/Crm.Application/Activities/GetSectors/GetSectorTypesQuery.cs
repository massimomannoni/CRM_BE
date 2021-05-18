
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Activities.GetSectors
{
    public class GetSectorTypesQuery : IRequest<List<SectorTypesDto>>
    {
        public GetSectorTypesQuery()
        {
          
        }
    }
}
