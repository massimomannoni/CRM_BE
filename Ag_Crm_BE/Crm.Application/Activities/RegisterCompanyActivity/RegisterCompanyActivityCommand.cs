using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Activities
{
    public class RegisterCompanyActivityCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public string ActivityType { get; set; }

        public string SectorType { get; set; }

        public bool Value { get; set; }


        public RegisterCompanyActivityCommand(Guid companyId, string activityType,  string sectorType, bool value )
        {
            CompanyId = companyId;
            ActivityType = activityType;
            SectorType = sectorType;
            Value = value;
        }
    }
}
