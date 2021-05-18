using Crm.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Application.Companies.ChangeCompany
{
    public class RemoveCompanyCommand : CommandBase<Unit>
    {
        public Guid CompanyId { get; set; }

        public RemoveCompanyCommand(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
