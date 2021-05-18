using Crm.Domain.Companies;
using Crm.Infrastructure.Processing.InternalCommands;
using Microsoft.EntityFrameworkCore;


namespace Crm.Infrastructure.Database
{
    public class CompaniesContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

        public CompaniesContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompaniesContext).Assembly);
        }
    }
}
