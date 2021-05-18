using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Addresses.GetCompanyAddresses
{
    public class GetCompanyAddressesQueryHandler : IRequestHandler<GetCompanyAddressesQuery, List<AddressesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyAddressesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<AddressesDto>> Handle(GetCompanyAddressesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string addressesSql = "SELECT  [Id], [AddressType], [Value], [IsRemoved] FROM [AG_Crm].[Web].[Addresses] WHERE [CompanyId] = @CompanyId And [IsRemoved] = 'false'";

            var addresses = await connection.QueryAsync<AddressesDto>(addressesSql, new { request.CompanyId });

            return addresses.AsList();
        }
    }
}
