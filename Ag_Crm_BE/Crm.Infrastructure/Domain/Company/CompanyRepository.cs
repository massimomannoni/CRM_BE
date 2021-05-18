
using Crm.Domain.Companies;
using Crm.Infrastructure.Database;
using Crm.Infrastructure.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Crm.Infrastructure.Domain.Compananies
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompaniesContext _context;
        public CompanyRepository(CompaniesContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAsync(Company company)
        {
            await _context.Companies.AddAsync(company);
        }

        public async Task<Company> GetByIdAsync(CompanyId id)
        {
            Stopwatch timer = new Stopwatch();

            timer.Start();

            var company = await _context.Companies
                    .Include(CompanyEntityTypeConfiguration.AddressesList)
                    .Include(CompanyEntityTypeConfiguration.EmployeeList)
                    .Include(CompanyEntityTypeConfiguration.ActivitiesList)
                    .Include(CompanyEntityTypeConfiguration.DimensionList)
                    .Include(CompanyEntityTypeConfiguration.EmployeeOverViewList)
                    .SingleOrDefaultAsync(x => x.Id == id);


            timer.Stop();

            Trace.WriteLine(timer.ElapsedMilliseconds);

            return company;
        }

        public void Remove(Company company)
        {
            _context.Remove(company);
        }
    }
}
