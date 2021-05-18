using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crm.Application.Activities
{
    public class RegisterCompantActivityRequest
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string ActivityType { get; set; }

        [Required]
        public string SectorType { get; set; }

        [Required]
        public bool Value { get; set; }

    }
}
