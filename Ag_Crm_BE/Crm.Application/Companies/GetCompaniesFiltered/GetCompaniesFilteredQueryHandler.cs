using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Companies.GetCompanyDetails;

namespace Crm.Application.Companies.GetCompaniesFiltered
{
    public class GetCompaniesFilteredQueryHandler : IRequestHandler<GetCompaniesFilteredQuery, List<CompanyDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompaniesFilteredQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<CompanyDetailsDto>> Handle(GetCompaniesFilteredQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string companiesFilteredSql = "SELECT [Id] ,[Name] ,[Address], [Cap] ,[City] ,[Province] ,[FiscalCode],[Piva] ,[ContractType],[SubscriptionType],[SubscriptionDate],[CreationDate] FROM [AG_Crm].[Web].[Companies] WHERE [name] like '% @Filter &' OR [City] like '% @Filter &' OR [Province]  like '% @Filter &' OR [FiscalCode] like '% @Filter &' OR [PIva] like '% @Filter &' OR [SubScriptionType] like '% @Filter &'";

            var companies = await connection.QueryAsync<CompanyDetailsDto>(companiesFilteredSql, new { request.Filter });

            return companies.AsList();
        }
    }
}
