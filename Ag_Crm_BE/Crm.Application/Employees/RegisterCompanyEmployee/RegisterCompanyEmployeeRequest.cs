using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Application.Employees
{
    public class RegisterCompanyEmployeeRequest
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string ContactType { get; set; }

    }
}
