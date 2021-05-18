using Crm.Application.Companies.GetCompanyDetails;
using MediatR;
using System.Collections.Generic;

namespace Crm.Application.Companies.GetCompaniesFiltered
{
    public class GetCompaniesFilteredQuery : IRequest<List<CompanyDetailsDto>>
    {
        public string Filter { get; set; }
        public GetCompaniesFilteredQuery(string filter)
        {
            Filter = filter;
        }
    }
}
