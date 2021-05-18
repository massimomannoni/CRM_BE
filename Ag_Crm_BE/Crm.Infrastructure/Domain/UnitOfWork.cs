using Crm.Domain.SeedWork;
using Crm.Infrastructure.Database;
using Crm.Infrastructure.Processing;
using System.Threading;
using System.Threading.Tasks;

namespace Crm.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompaniesContext _companiesContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork( CompaniesContext companiesContext,  IDomainEventsDispatcher domainEventsDispatcher)
        {
            _companiesContext = companiesContext;
            _domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _companiesContext.SaveChangesAsync(cancellationToken);
        }
    }
}