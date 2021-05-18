using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Dimensions
{
    public class RegisterCompanyDimensionCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public string DimensionType { get; set; }

        public decimal Fee { get; set; }

        public RegisterCompanyDimensionCommand(Guid companyId, string dimensionType, decimal fee)
        {
            CompanyId = companyId;
            DimensionType = dimensionType;
            Fee = fee;
        }
    }
}
