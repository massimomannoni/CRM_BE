using System;

namespace Crm.Application.EmployeesOverViews
{
    public class EmployeesOverViewDto
    {
        public Guid Id { get; set; }

        public string ContractLevelType { get; set; }

        public short Employees { get; set; }

        public bool IsRemoved { get; set; }
    }
}
