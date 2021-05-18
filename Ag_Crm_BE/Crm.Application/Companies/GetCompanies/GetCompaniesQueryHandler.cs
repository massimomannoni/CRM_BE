using MediatR;
using Crm.Application.Configuration.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Crm.Application.Companies.GetCompanyDetails;

namespace Crm.Application.Companies.GetConpanies
{
    public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesQuery, List<CompanyDetailsDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        public GetCompaniesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        public async Task<List<CompanyDetailsDto>> Handle(GetCompaniesQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string companiesSql = "SELECT [Id] ,[Name] ,[Address], [Cap] ,[City] ,[Province] ,[FiscalCode],[Piva] ,[ContractType],[SubscriptionType],[SubscriptionDate],[CreationDate] FROM [AG_Crm].[Web].[Companies]";

            var companies = await connection.QueryAsync<CompanyDetailsDto>(companiesSql);

            return companies.AsList();
        }
    }
}
