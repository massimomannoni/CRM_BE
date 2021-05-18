using Crm.Application.Configuration.Commands;
using MediatR;
using System;


namespace Crm.Application.Contacts
{
    public class RemoveEmployeeContactCommand : CommandBase<Unit>
    {
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid ContactId { get; set; }

        public RemoveEmployeeContactCommand(Guid companyId, Guid employeeId, Guid contactId)
        {

            CompanyId = companyId;
            EmployeeId = employeeId;
            ContactId = contactId;
        }
    }
}
