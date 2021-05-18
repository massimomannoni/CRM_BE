
using MediatR;

using System.Collections.Generic;

namespace Crm.Application.Employees.GetEmployees
{
    public class GetEmployeesQuery : IRequest<List<EmployeeXDto>>
    {
        public GetEmployeesQuery()
        {

        }
    }
}
