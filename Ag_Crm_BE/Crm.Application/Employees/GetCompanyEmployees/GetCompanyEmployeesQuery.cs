using Crm.Application.Employees;
using MediatR;
using System;
using System.Collections.Generic;

namespace Crm.Application.Companies.Employees.GetCompanyEmployees
{
    public class GetCompanyEmployeesQuery : IRequest<List<EmployeeDto>>
    {
        public Guid CompanyId { get; set; }
        public GetCompanyEmployeesQuery(Guid companyId)
        {
            CompanyId = companyId;
        }
    }
}
