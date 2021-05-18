using Crm.Application.Activities;
using Crm.Application.Addresses;
using Crm.Application.Contacts;
using Crm.Application.Configuration.Data;
using Crm.Application.Configuration.Queries;
using Dapper;
using System.Threading;
using System.Threading.Tasks;
using Crm.Application.Employees;
using System.Linq;
using Crm.Application.Dimensions;
using Crm.Application.EmployeesOverViews;

namespace Crm.Application.Companies.GetCompanyDetails
{
    public class GetCompanyDetailsQueryHandler : IQueryHandler<GetCompanyDetailsQuery, CompanyDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompanyDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<CompanyDetailsDto> Handle(GetCompanyDetailsQuery request, CancellationToken cancellationToken)
        {
            const string companySql = "SELECT [Id] ,[Name] ,[Address], [Cap] ,[City],[Province] ,[FiscalCode],[Piva],[ContractType],[SubScriptionType],[SubScriptionDate],[CreationDate] FROM [AG_Crm].[Web].[Companies] WHERE [Companies].[Id] = @CompanyId ";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            var company = await connection.QuerySingleOrDefaultAsync<CompanyDetailsDto>(companySql, new { request.CompanyId });

            const string activitiesSql = "SELECT  [Id], [ActivityType], [SectorType], [Value] FROM [AG_Crm].[Web].[Activities] WHERE [CompanyId] = @CompanyId AND [IsRemoved] = 'false'";

            var activies = await connection.QueryAsync<ActivityDto>(activitiesSql, new { request.CompanyId });

            company.Activities = activies.AsList();


            const string addressesSql = "SELECT  [Id], [AddressType], [Value] FROM [AG_Crm].[Web].[GetCompanyAddresses](@CompanyId) WHERE [IsRemoved] = 'false'";

            var addresses = await connection.QueryAsync<AddressesDto>(addressesSql, new { request.CompanyId });

            company.Addresses = addresses.AsList();


            const string employeesSql = "SELECT  [Id], [Name], [Surname], [ContactType] FROM [AG_Crm].[Web].[Employees] WHERE [CompanyId] = @CompanyId AND [IsRemoved] = 'false'";

            var employees = await connection.QueryAsync<EmployeeDto>(employeesSql, new { request.CompanyId });

            company.Employees = employees.AsList();


            const string contactsSql = "SELECT  [Id], [EmployeeId], [AddressType], [Value] FROM  [Web].[GetCompanyContacts](@CompanyId) WHERE [IsRemoved] = 'false'";

            var contacts = await connection.QueryAsync<ContactDto>(contactsSql, new { request.CompanyId });

            foreach (var employee in employees)
            {
                employee.Contacts = contacts.Where(e => e.EmployeeId == employee.Id).ToList();                
            }


            const string dimensionSql = "SELECT  [Id], [DimensionType] , [Fee] FROM [AG_Crm].[Web].[Dimensions] WHERE [CompanyId] = @CompanyId AND [IsRemoved] = 'false'";

            var dimensions = await connection.QueryAsync<DimensionDto>(dimensionSql, new { request.CompanyId });

            company.Dimensions = dimensions.AsList();


            const string employeesOverViewSql = "SELECT  [Id], [ContractLevelType] , [Employees] FROM [AG_Crm].[Web].[EmployeesOverView] WHERE [CompanyId] = @CompanyId AND [IsRemoved] = 'false'";

            var employeesOverViews = await connection.QueryAsync<EmployeesOverViewDto>(employeesOverViewSql, new { request.CompanyId });

            company.EmployeesOverViews = employeesOverViews.AsList();

            return company;
        }
    }
}
