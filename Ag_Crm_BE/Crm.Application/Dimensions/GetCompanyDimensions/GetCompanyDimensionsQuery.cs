
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Dimensions.GetCompanyDimensions
{
    public class GetCompanyDimensionsQuery : IRequest<List<DimensionDto>>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyDimensionsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
