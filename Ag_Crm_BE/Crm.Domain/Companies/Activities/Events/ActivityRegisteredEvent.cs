using Crm.Domain.Companies;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Domain.Companies.Activities.Events
{
    public class ActivityRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public ActivityId ActivityId { get; }

        public ActivityRegisteredEvent(ActivityId activityId, CompanyId companyId)
        {
            ActivityId = activityId;
            CompanyId = companyId;
        }
    }
}
