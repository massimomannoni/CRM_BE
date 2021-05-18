
using System.ComponentModel.DataAnnotations;


namespace Crm.Application.Companies.GetCompaniesFiltered
{
    public class FilterCompanyRequest
    {

        [Required]
        public string Filter { get; set; }
    }
}
