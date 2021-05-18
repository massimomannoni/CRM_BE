using System;
using System.Collections.Generic;
using System.Text;
using Crm.Domain.SeedWork;

namespace Crm.Domain.Companies
{
    public class CompanyId : TypedIdValueBase
    {
        public CompanyId(Guid value) : base(value)
        {

        }
    }
}
