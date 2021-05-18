using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Employees;
using Crm.Application.Contacts;
using System.Linq;

namespace Crm.Application.Companies.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQueryHandler : IRequestHandler<GetCompanyEmployeesQuery, List<EmployeeDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyEmployeesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<EmployeeDto>> Handle(GetCompanyEmployeesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string employeeSql = "SELECT  [Id], [Name], [Surname], [ContactType], [IsRemoved] FROM [AG_Crm].[Web].[Employees] WHERE [CompanyId] = @CompanyId And [IsRemoved] = 'false'";

            var employees = await connection.QueryAsync<EmployeeDto>(employeeSql, new { request.CompanyId });

            const string contactsSql = "SELECT  [Id], [EmployeeId], [AddressType], [Value] FROM  [Web].[GetCompanyContacts](@CompanyId) WHERE [IsRemoved] = 'false'";

            var contacts = await connection.QueryAsync<ContactDto>(contactsSql, new { request.CompanyId });

            foreach (var employee in employees)
            {
                employee.Contacts = contacts.Where(e => e.EmployeeId == employee.Id).ToList();
            }
            return employees.AsList();
        }
    }
}
