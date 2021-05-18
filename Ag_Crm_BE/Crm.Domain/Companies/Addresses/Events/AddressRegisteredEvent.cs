using Crm.Domain.Companies;
using Crm.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Domain.Companies.Addresses.Events
{
    public class AddressRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public AddressId AddressId { get; }

        public AddressRegisteredEvent(AddressId addressId, CompanyId companyId)
        {
            AddressId = addressId;
            CompanyId = companyId;
        }
    }
}
