using Crm.Application.Companies.GetCompanyDetails;
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Companies.GetConpanies
{
    public class GetCompaniesQuery : IRequest<List<CompanyDetailsDto>>
    {
        public GetCompaniesQuery()
        {
        }

    }
}
