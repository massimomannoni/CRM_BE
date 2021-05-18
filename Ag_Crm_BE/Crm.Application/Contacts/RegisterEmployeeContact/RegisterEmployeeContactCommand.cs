using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Contacts
{
    public class RegisterEmployeeContactCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }

        public string AddressType { get; set; }

        public string Value { get; set; }


        public RegisterEmployeeContactCommand(Guid companyId, Guid employeeId, string addressType, string value )
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            AddressType = addressType;
            Value = value;
        }
    }
}
