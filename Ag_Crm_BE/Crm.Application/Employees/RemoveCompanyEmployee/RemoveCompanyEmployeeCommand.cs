using Crm.Application.Configuration.Commands;
using MediatR;
using System;


namespace Crm.Application.Employees
{
    public class RemoveCompanyEmployeeCommand : CommandBase<Unit>
    {
        public Guid EmployeeId { get; set; }

        public Guid CompanyId { get; set; }

        public RemoveCompanyEmployeeCommand(Guid employeeId, Guid companyId)
        {
            EmployeeId = employeeId;
            CompanyId = companyId;
        }
    }
}
