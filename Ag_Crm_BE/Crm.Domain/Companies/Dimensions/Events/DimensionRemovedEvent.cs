using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Dimensions.Events
{
    public class DimensionRemovedEvent : DomainEventBase
    {
        public DimensionId DimensionId { get; }

        public DimensionRemovedEvent(DimensionId dimensionId)
        {
            DimensionId = dimensionId;
        }
    }
}
