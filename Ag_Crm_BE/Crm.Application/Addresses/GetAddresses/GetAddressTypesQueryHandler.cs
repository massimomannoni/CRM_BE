using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Addresses.GetAddresses
{
    public class GetAddressTypesQueryHandler : IRequestHandler<GetAddressTypesQuery, List<AddressTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetAddressTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<AddressTypesDto>> Handle(GetAddressTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string addressesSql = "SELECT [Id], [Name] FROM [Reference].[AddressType]";

            var addressTypes = await connection.QueryAsync<AddressTypesDto>(addressesSql); 

            return addressTypes.AsList();
        }
    }
}
