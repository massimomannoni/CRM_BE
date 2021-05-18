using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Dimensions.GetCompanyDimensions;
using Crm.Application.Dimensions;

namespace Crm.Application.Addresses.GetCompanyAddresses
{
    public class GetCompanyDimensionsQueryHandler : IRequestHandler<GetCompanyDimensionsQuery, List<DimensionDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyDimensionsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<DimensionDto>> Handle(GetCompanyDimensionsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string dimensionsSql = "SELECT  [Id], [DimensionType], [Fee], [IsRemoved] FROM [AG_Crm].[Web].[Dimensions] WHERE [CompanyId] = @CompanyId And [IsRemoved] = 'false'";

            var dimensions = await connection.QueryAsync<DimensionDto>(dimensionsSql, new { request.CompanyId });

            return dimensions.AsList();
        }
    }
}
