using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Activities.Events
{
    public class ActivityRemovedEvent : DomainEventBase
    {
        public ActivityId ActivityId { get; }

        public ActivityRemovedEvent(ActivityId contactId)
        {
            ActivityId = contactId;
        }
    }
}
