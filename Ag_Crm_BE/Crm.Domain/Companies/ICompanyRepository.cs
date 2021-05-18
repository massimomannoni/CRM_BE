using System.Threading.Tasks;

namespace Crm.Domain.Companies
{
    public interface ICompanyRepository
    {
        Task CreateAsync(Company company);

        Task<Company> GetByIdAsync(CompanyId id);

        void Remove (Company company);
    }
}
