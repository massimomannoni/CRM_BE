using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.Employees.GetEmployees
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeXDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetEmployeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<EmployeeXDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string contactsSql = "SELECT [Id], [Name], [Surname]. [ContactType] FROM [Web].[Employees]";

            var contactTypes = await connection.QueryAsync<EmployeeXDto>(contactsSql); 

            return contactTypes.AsList();
        }
    }
}
