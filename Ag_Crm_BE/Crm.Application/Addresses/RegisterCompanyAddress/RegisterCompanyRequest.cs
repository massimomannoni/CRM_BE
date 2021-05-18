using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Application.Addresses
{
    public class RegisterCompanyAddressRequest
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public string Value { get; set; }

    }
}
