using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace Crm.Application.EmployeesOverViews.GetCompanyEmployeesOverView
{
    public class GetCompanyEmployeeOverViewQueryHandler : IRequestHandler<GetCompanyEmployeeOverViewQuery, List<EmployeesOverViewDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyEmployeeOverViewQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<EmployeesOverViewDto>> Handle(GetCompanyEmployeeOverViewQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string employeesOverViewSql = "SELECT  [Id], [ContractLevelType], [Employees], [IsRemoved] FROM [AG_Crm].[Web].[EmployeesOverView] WHERE [CompanyId] = @CompanyId And [IsRemoved] = 'false'";

            var employeesOverView = await connection.QueryAsync<EmployeesOverViewDto>(employeesOverViewSql, new { request.CompanyId });

            return employeesOverView.AsList();
        }
    }
}
