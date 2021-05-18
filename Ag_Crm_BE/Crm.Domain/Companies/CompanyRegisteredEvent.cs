using Crm.Domain.SeedWork;
using Crm.Domain.Companies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Domain.Companies
{
    public class CompanyRegisteredEvent : DomainEventBase
    {
        public CompanyId CompanyId { get; }

        public CompanyRegisteredEvent(CompanyId companyId)
        {
            CompanyId = companyId;
        }
    }
}
