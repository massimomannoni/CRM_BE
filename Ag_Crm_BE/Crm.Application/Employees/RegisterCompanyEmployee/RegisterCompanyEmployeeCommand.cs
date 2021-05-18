using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Employees
{
    public class RegisterCompanyEmployeeCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ContactType { get; set; }


        public RegisterCompanyEmployeeCommand(Guid companyId, string name, string surname, string contactType )
        {
            CompanyId = companyId;
            Name = name;
            Surname = surname;
            ContactType = contactType;
        }
    }
}
