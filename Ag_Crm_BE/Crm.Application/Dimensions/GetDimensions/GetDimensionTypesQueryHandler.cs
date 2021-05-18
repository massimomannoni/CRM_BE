using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Dimensions.GetDimensions
{
    public class GetDimensionTypesQueryHandler : IRequestHandler<GetDimensionTypesQuery, List<DimensionTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetDimensionTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<DimensionTypesDto>> Handle(GetDimensionTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string dimensionsTypesSql = "SELECT [Id], [ContractType], [Name], [Fee] FROM [Reference].[DimensionType]";

            var dimensionsTypes = await connection.QueryAsync<DimensionTypesDto>(dimensionsTypesSql); 

            return dimensionsTypes.AsList();
        }
    }
}
