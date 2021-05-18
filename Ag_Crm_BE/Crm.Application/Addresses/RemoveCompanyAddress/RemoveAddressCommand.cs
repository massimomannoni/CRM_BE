using Crm.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Application.Addresses
{
    public class RemoveAddressCommand : CommandBase<Unit>
    {
        public Guid AddressId { get; set; }

        public Guid CompanyId { get; set; }

        public RemoveAddressCommand(Guid addressId, Guid companyId)
        {
            AddressId = addressId;
            CompanyId = companyId;
        }
    }
}
