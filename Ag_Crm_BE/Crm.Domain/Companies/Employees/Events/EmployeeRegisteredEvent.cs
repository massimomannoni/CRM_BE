using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Employees.Events
{
    public class EmployeeRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public EmployeeId EmployeeId { get; }

        public EmployeeRegisteredEvent(EmployeeId employeeId, CompanyId companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}
