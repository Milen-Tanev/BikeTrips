[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BikeTrips.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BikeTrips.Web.App_Start.NinjectWebCommon), "Stop")]

namespace BikeTrips.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data;
    using Data.Common.Contracts;
    using Services.Data.Contracts;
    using Services.Data;
    using Services.Web.Contracts;
    using Services.Web;
    using Data.Common;
    using System.Data;
    using System.Data.Common;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Data.Models;
    using System.Data.Entity;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // My binds
                kernel.Bind<BikeTripsDbContext>().ToSelf().InRequestScope();

                kernel.Bind<IUserService>().To<UsersService>().InRequestScope();
                kernel.Bind<ITripsService>().To<TripsService>().InRequestScope();
                kernel.Bind<ICommentsService>().To<CommentsService>().InRequestScope();

                kernel.Bind<IDbConnection>().To<DbConnection>().InRequestScope();
                kernel.Bind<DbContext>().To<BikeTripsDbContext>().InRequestScope().WithConstructorArgument("DefaultConnection");
                kernel.Bind<ICacheService>().To<HttpCacheService>().InRequestScope();
                kernel.Bind<IDateTimeConverter>().To<DateTimeConverter>().InRequestScope();
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<IIdentifierProvider>().To<IdentifierProvider>().InRequestScope();

                kernel.Bind(typeof(IBikeTripsDbRepository<>)).To(typeof(BikeTripsDbRepository<>)).InRequestScope();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
