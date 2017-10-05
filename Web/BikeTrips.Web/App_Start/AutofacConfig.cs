using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using BikeTrips.Data;
using BikeTrips.Data.Common;
using BikeTrips.Data.Common.Contracts;
using BikeTrips.Services.Data;
using BikeTrips.Services.Data.Contracts;
using BikeTrips.Services.Web;
using BikeTrips.Services.Web.Contracts;
using BikeTrips.Utils;
using BikeTrips.Web.Hubs;
using Microsoft.AspNet.SignalR;
using System.Data.Entity;
using System.Reflection;
using System.Web.Mvc;

namespace BikeTrips.Web.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterAutofac()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            //SignalR
            //GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            Guard.ThrowIfNull(builder, "Builder");
            // Configure the db context, user manager and signin manager to use a single instance per request
            builder.RegisterType<BikeTripsDbContext>().AsSelf().InstancePerRequest();
            builder.Register(c => c.Resolve<BikeTripsDbContext>()).As<DbContext>().InstancePerRequest();
            var servicesAssembly = Assembly.GetAssembly(typeof(ITripsService));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();
            builder.Register(x => new HttpCacheService()).As<ICacheService>().InstancePerRequest();
            builder.Register(x => new DateTimeConverter()).As<IDateTimeConverter>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.Register(x => new IdentifierProvider()).As<IIdentifierProvider>().InstancePerRequest();


            //SignalR !!!!!
            //builder.RegisterType<ChatHub>().ExternallyOwned();
            //builder.Register(x => new ChatService()).As<IChatService>().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(BikeTripsDbRepository<>)).As(typeof(IBikeTripsDbRepository<>)).InstancePerRequest();
        }

        public static void RegisterAutofacSignalR()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServicesSignalR(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            //DependencyResolver.SetResolver(new Autofac.Integration.Mvc.AutofacDependencyResolver(container));

            //SignalR
            GlobalHost.DependencyResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
        }

        private static void RegisterServicesSignalR(ContainerBuilder builder)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            builder.RegisterType<BikeTripsDbContext>().AsSelf().InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<BikeTripsDbContext>()).As<DbContext>().InstancePerLifetimeScope();
            var servicesAssembly = Assembly.GetAssembly(typeof(ITripsService));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();
            builder.Register(x => new HttpCacheService()).As<ICacheService>().InstancePerLifetimeScope();
            builder.Register(x => new DateTimeConverter()).As<IDateTimeConverter>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.Register(x => new IdentifierProvider()).As<IIdentifierProvider>().InstancePerLifetimeScope();
            
            builder.RegisterType<ChatHub>().ExternallyOwned();

            builder.RegisterGeneric(typeof(BikeTripsDbRepository<>)).As(typeof(IBikeTripsDbRepository<>)).ExternallyOwned();
        }

    }
}