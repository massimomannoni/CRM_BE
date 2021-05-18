using Microsoft.Extensions.DependencyInjection;
using Crm.Infrastructure.Database;
using System;
using CommonServiceLocator;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using Autofac.Core;
using Crm.Infrastructure.Processing;
using Crm.Application.Configuration;
using Crm.Infrastructure.Caching;

namespace Crm.Infrastructure
{
    public class ApplicationStartup
    {
        public static IServiceProvider Initialize(IServiceCollection services, string connectionString, ICacheStore cacheStore, IExecutionContextAccessor executionContextAccessor)
        {

            services.AddSingleton(cacheStore);

            return RegisterAutofacServiceProvider(services, connectionString,  executionContextAccessor);
        }

        private static IServiceProvider RegisterAutofacServiceProvider(IServiceCollection services, string connectionString, IExecutionContextAccessor executionContextAccessor)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);

            containerBuilder.RegisterModule(new DataAccessModule(connectionString));
            containerBuilder.RegisterModule(new MediatorModule());
            containerBuilder.RegisterModule(new ProcessingModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            var containerBuilded = containerBuilder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(containerBuilded));

            var autofacServiceProvider = new AutofacServiceProvider(containerBuilded);

            CompositionRoot.SetContainer(containerBuilded);

            return autofacServiceProvider;
        }
    }

}
