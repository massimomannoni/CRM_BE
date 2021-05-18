
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.EmployeesOverViews.GetCompanyEmployeesOverView
{
    public class GetCompanyEmployeeOverViewQuery : IRequest<List<EmployeesOverViewDto>>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyEmployeeOverViewQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
