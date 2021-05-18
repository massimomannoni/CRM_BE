using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Application.EmployeesOverViews
{
    public class RegisterCompanyEmployeesOverViewRequest
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string ContractLevelType { get; set; }

        [Required]
        public short Employees { get; set; }
    }
}
