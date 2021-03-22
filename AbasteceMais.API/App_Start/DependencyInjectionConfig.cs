using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using AbasteceMais.Data.Context;
using AbasteceMais.Data.Repositories;
using AbasteceMais.Data.UnitOfWork;
using AbasteceMais.Domain.Interfaces.Repositories;
using AbasteceMais.Domain.Interfaces.Services;
using AbasteceMais.Services.Service;
using AbasteceMais.Domain.Interfaces.UnitOfWork;
using AbasteceMais.Domain.Entities;

namespace AbasteceMais.API
{
    public class DependencyInjectionConfig
    {
        public static void Config()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // Get your HttpConfiguration.
            HttpConfiguration httpConfiguration = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register concrete types against interfaces

            containerBuilder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));

            containerBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            containerBuilder.RegisterType<DatabaseContextAbasteceMais>().AsSelf();

            containerBuilder.RegisterType<RegistrationsService>().As<IRegistrationsService>();


            //// OPTIONAL: Register the Autofac filter provider.
            //builder.RegisterWebApiFilterProvider(config);

            //// OPTIONAL: Register the Autofac model binder provider.
            //builder.RegisterWebApiModelBinderProvider();

            // Set the dependency resolver to be Autofac.
            IContainer container = containerBuilder.Build();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
