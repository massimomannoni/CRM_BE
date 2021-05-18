using Crm.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Application.Companies.GetCompanyDetails
{
    public class GetCompanyDetailsQuery : IQuery<CompanyDetailsDto>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyDetailsQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
