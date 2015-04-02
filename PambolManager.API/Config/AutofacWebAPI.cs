using Autofac;
using Autofac.Integration.WebApi;
using PambolManager.Domain.Entities.Core;
using PambolManager.Domain.Services;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;

namespace PambolManager.API.Config
{
    public class AutofacWebAPI
    {
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            // Register EF DbContext
            builder.RegisterType<EntitiesContext>().As<DbContext>().InstancePerRequest();

            // Register repositories by using Autofac's OpenGenerics feature
            // More info: http://code.google.com/p/autofac/wiki/OpenGenerics
            builder.RegisterGeneric(typeof(EntityRepository<>))
                .As(typeof(IEntityRepository<>))
                .InstancePerRequest();

            // Register services
            builder.RegisterType<ManagementService>()
                .As<IManagementService>()
                .InstancePerRequest();

            return builder.Build();
        }
    }
}
