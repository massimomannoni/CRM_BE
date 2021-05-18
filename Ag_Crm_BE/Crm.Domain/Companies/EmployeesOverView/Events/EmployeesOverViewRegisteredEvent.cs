using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.EmployeesOverViews.Events
{
    public class EmployeesOverViewRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public EmployeesOverViewId EmployeesOverViewId { get; }

        public EmployeesOverViewRegisteredEvent(EmployeesOverViewId employeesOverViewId, CompanyId companyId)
        {
            EmployeesOverViewId = employeesOverViewId;
            CompanyId = companyId;
        }
    }
}
