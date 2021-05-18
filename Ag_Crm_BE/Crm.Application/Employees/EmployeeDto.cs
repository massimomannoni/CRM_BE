using Crm.Application.Contacts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crm.Application.Employees
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ContactType { get; set; }

        public List<ContactDto> Contacts { get; set; }

        public bool IsRemoved { get; set; }
    }
}
