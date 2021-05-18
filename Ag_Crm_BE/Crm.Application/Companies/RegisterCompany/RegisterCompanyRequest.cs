using System;
using System.ComponentModel.DataAnnotations;


namespace Crm.Application.Companies
{
    public class RegisterCompanyRequest
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string FiscalCode { get; set; }

        [Required]
        public string PIva { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Cap { get; set; }

        [Required]
        public string ContractType { get; set; }


        [Required]
        public string SubScriptionType { get; set; }


        [Required]
        public DateTime SubScriptionDate { get; set; }
    }
}
