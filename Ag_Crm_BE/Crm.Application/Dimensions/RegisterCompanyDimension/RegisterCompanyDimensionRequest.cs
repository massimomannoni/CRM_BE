using System;
using System.ComponentModel.DataAnnotations;

namespace Crm.Application.Dimensions
{
    public class RegisterCompanyDimensionRequest
    {
        [Required]
        public Guid CompanyId { get; set; }

        [Required]
        public string DimensionType { get; set; }

        [Required]
        public decimal Fee { get; set; }

    }
}
