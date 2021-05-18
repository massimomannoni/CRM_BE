
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Addresses.GetCompanyAddresses
{
    public class GetCompanyAddressesQuery : IRequest<List<AddressesDto>>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyAddressesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
