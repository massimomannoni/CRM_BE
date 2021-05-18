using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Activities.GetCompanyActivities
{
    public class GetCompanyAddressesQueryHandler : IRequestHandler<GetCompanyActivitiesQuery, List<ActivityDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyAddressesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ActivityDto>> Handle(GetCompanyActivitiesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string addressesSql = "SELECT  [Id], [ActivityType], [SectorType], [Value], [IsRemoved] FROM [AG_Crm].[Web].[Activities] WHERE [CompanyId] = @CompanyId And [IsRemoved] = 'false'";

            var addresses = await connection.QueryAsync<ActivityDto>(addressesSql, new { request.CompanyId });

            return addresses.AsList();
        }
    }
}
