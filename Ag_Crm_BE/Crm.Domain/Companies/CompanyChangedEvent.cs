using Crm.Domain.SeedWork;


namespace Crm.Domain.Companies
{
    public class CompanyChangedEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public CompanyChangedEvent(CompanyId companyId)
        {
            CompanyId = companyId;
        }
    }
}
