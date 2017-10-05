namespace BikeTrips.Web
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using App_Start;
    using Data;
    using Data.Migrations;
    using Infrastructure.Mappings;


    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BikeTripsDbContext, Configuration>());
            AutofacConfig.RegisterAutofac();
            AutofacConfig.RegisterAutofacSignalR();
            AutoMapperConfig automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
