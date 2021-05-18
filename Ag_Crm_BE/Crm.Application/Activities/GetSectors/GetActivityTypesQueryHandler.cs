using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Activities.GetSectors;

namespace Crm.Application.Activities.GetActivities
{
    public class GetSectorTypesQueryHandler : IRequestHandler<GetSectorTypesQuery, List<SectorTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetSectorTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<SectorTypesDto>> Handle(GetSectorTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sectorTypesSql = "SELECT [Id], [Name] FROM [Reference].[SectorType]";

            var sectorTypes = await connection.QueryAsync<SectorTypesDto>(sectorTypesSql); 

            return sectorTypes.AsList();
        }
    }
}
