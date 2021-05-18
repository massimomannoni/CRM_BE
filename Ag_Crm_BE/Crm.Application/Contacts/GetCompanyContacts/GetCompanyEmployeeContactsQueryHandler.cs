using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Contacts.GetCompanyEmployeeContacts
{
    public class GetCompanyEmployeeContactsQueryHandler : IRequestHandler<GetCompanyEmployeeContactsQuery, List<ContactDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyEmployeeContactsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<ContactDto>> Handle(GetCompanyEmployeeContactsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string contactsSql = "SELECT  [Id], [ContactType], [AddressType], [Value], [IsRemoved] FROM [AG_Crm].[Web].[Contacts] WHERE [EmployeeId] = @EmployeeId And [IsRemoved] = 'false'";

            var contacts = await connection.QueryAsync<ContactDto>(contactsSql, new { request.EmployeeId });

            return contacts.AsList();
        }
    }
}
