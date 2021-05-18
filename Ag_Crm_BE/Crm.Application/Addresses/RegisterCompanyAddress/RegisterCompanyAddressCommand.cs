using Crm.Application.Configuration.Commands;
using System;

namespace Crm.Application.Addresses
{
    public class RegisterCompanyAddressCommand : CommandBase<Guid>
    {
        public Guid CompanyId { get; set; }

        public string AddressType { get; set; }

        public string Value { get; set; }


        public RegisterCompanyAddressCommand(Guid companyId, string addressType, string value )
        {
            CompanyId = companyId;
            AddressType = addressType;
            Value = value;
        }
    }
}
