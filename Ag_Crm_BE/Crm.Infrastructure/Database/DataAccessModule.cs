using Autofac;
using Crm.Application.Configuration.Data;
using Crm.Domain.Companies;
using Crm.Domain.SeedWork;
using Crm.Infrastructure.Domain;
using Crm.Infrastructure.Domain.Compananies;
using Crm.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Crm.Infrastructure.Database
{
    public class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;

        public DataAccessModule(string connectionString)
        {
            _databaseConnectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
             .As<ISqlConnectionFactory>()
             .WithParameter("connectionString", _databaseConnectionString)
             .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
             .As<IUnitOfWork>()
             .InstancePerLifetimeScope();

            builder.RegisterType<CompanyRepository>()
            .As<ICompanyRepository>()
            .InstancePerLifetimeScope();

            builder.RegisterType<StronglyTypedIdValueConverterSelector>()
               .As<IValueConverterSelector>()
               .InstancePerLifetimeScope();

            builder
             .Register(c =>
             {
                 var dbContextOptionsBuilder = new DbContextOptionsBuilder<CompaniesContext>();
                 dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                 .LogTo(Console.WriteLine, LogLevel.Information);
                 dbContextOptionsBuilder
                     .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                 return new CompaniesContext(dbContextOptionsBuilder.Options);
             })
             .AsSelf()
             .As<DbContext>()
             .InstancePerLifetimeScope();

        }
    }
}
