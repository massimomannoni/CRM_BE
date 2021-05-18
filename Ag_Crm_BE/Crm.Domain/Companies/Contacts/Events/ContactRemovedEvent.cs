
using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Contacts.Events
{
    public class ContactRemovedEvent : DomainEventBase
    {
        public ContactId ContactId { get; }

        public ContactRemovedEvent(ContactId contactId)
        {
            ContactId = contactId;
        }
    }
}
