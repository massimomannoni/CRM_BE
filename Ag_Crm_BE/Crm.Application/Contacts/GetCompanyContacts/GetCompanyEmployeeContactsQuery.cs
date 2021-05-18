using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Contacts.GetCompanyEmployeeContacts
{
    public class GetCompanyEmployeeContactsQuery : IRequest<List<ContactDto>>
    {
        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }
        public GetCompanyEmployeeContactsQuery(Guid companyId, Guid employeeId)
        {
            CompanyId = companyId;

            EmployeeId = employeeId;
        }
    }
}
