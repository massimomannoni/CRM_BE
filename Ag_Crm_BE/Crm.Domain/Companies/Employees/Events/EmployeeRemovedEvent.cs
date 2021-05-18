
using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Employees.Events
{
    public class EmployeeRemovedEvent : DomainEventBase
    {
        public EmployeeId EmployeeId { get; }

        public EmployeeRemovedEvent(EmployeeId employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
