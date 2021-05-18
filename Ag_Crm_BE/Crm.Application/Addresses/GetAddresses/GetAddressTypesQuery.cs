
using MediatR;

using System.Collections.Generic;

namespace Crm.Application.Addresses.GetAddresses
{
    public class GetAddressTypesQuery : IRequest<List<AddressTypesDto>>
    {
        public GetAddressTypesQuery()
        {

        }
    }
}
