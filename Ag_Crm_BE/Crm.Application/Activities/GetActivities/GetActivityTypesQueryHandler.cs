using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Activities.GetActivities
{
    public class GetActivityTypesQueryHandler : IRequestHandler<GetActivityTypesQuery, List<ActivityTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetActivityTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ActivityTypesDto>> Handle(GetActivityTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string activityTypesSql = "SELECT [Id], [Name] FROM [Reference].[ActivityType]";

            var activityTypes = await connection.QueryAsync<ActivityTypesDto>(activityTypesSql); 

            return activityTypes.AsList();
        }
    }
}
