using Crm.Domain.Companies.Employees;
using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies.Contacts.Events
{
    public class ContactRegisteredEvent : DomainEventBase
    {
        public EmployeeId EmployeeId { get; }

        public ContactId ContactId { get; }

        public ContactRegisteredEvent(ContactId contactId, EmployeeId employeeId)
        {
            ContactId = contactId;
            EmployeeId = employeeId;
        }
    }
}
