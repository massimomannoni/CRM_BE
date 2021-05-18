using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.EmployeesOverViews
{
    public class RegisterCompanyEmployeesOverViewCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public string ContractLevelType { get; set; }

        public short Employees { get; set; }

        public RegisterCompanyEmployeesOverViewCommand(Guid companyId, string contractLevelType, short employees)
        {
            CompanyId = companyId;
            ContractLevelType = contractLevelType;
            Employees = employees;
        }
    }
}
