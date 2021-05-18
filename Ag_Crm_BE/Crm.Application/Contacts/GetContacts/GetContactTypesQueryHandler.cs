using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Contacts.GetContactTypes
{
    public class GetContactTypesQueryHandler : IRequestHandler<GetContactTypesQuery, List<ContactTypesDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetContactTypesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ContactTypesDto>> Handle(GetContactTypesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string contactsSql = "SELECT [Id], [Name] FROM [Reference].[ContactType]";

            var contactTypes = await connection.QueryAsync<ContactTypesDto>(contactsSql); 

            return contactTypes.AsList();
        }
    }
}
