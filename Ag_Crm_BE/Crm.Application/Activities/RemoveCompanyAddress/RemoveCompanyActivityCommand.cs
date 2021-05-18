using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Activities
{
    public class RemoveCompanyActivityCommand : CommandBase<Unit>
    {
       
        public Guid CompanyId { get; set; }

        public Guid ActivityId { get; set; }

        public RemoveCompanyActivityCommand(Guid companyId, Guid activityId)
        {
            CompanyId = companyId;
            ActivityId = activityId;
        }
    }
}
