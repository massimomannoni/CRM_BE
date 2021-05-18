using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.EmployeesOverViews
{
    public class RemoveCompanyEmployeesOverViewCommand : CommandBase<Unit>
    {
        public Guid EmployeesOverViewId { get; set; }

        public Guid CompanyId { get; set; }

        public RemoveCompanyEmployeesOverViewCommand(Guid employeesOverViewId, Guid companyId)
        {
            EmployeesOverViewId = employeesOverViewId;
            CompanyId = companyId;
        }
    }
}
