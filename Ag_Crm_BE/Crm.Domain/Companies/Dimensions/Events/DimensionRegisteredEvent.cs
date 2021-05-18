using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Dimensions.Events
{
    public class DimensionRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public DimensionId DimensionId { get; }

        public DimensionRegisteredEvent(DimensionId dimensionId, CompanyId companyId)
        {
            DimensionId = dimensionId;
            CompanyId = companyId;
        }
    }
}
