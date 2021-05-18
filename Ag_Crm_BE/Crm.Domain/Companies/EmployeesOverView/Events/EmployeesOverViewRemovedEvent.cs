using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.EmployeesOverViews.Events
{
    public class EmployeesOverViewRemovedEvent : DomainEventBase
    {
        public EmployeesOverViewId EmployeesOverViewId { get; }

        public EmployeesOverViewRemovedEvent(EmployeesOverViewId employeesOverViewId)
        {
            EmployeesOverViewId = employeesOverViewId;
        }
    }
}
