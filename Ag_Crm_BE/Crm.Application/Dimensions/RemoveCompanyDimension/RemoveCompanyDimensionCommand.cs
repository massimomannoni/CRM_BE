using Crm.Application.Configuration.Commands;
using MediatR;
using System;

namespace Crm.Application.Dimensions
{
    public class RemoveCompanyDimensionCommand : CommandBase<Unit>
    {
        public Guid DimensionId { get; set; }

        public Guid CompanyId { get; set; }

        public RemoveCompanyDimensionCommand(Guid dimensionId, Guid companyId)
        {
            DimensionId = dimensionId;
            CompanyId = companyId;
        }
    }
}
