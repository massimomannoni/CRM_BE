using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Application.Contacts
{
    public class RegisterEmployeeContactRequest
    {

        [Required]
        public string AddressType { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
