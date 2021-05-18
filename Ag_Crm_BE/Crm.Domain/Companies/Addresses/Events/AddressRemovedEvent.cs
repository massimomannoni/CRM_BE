using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Addresses.Events
{
    public class AddressRemovedEvent : DomainEventBase
    {
        public AddressId AddressId { get; }

        public AddressRemovedEvent(AddressId addressId)
        {
            AddressId = addressId;
        }
    }
}
