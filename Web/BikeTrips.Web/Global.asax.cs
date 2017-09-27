using BikeTrips.Data;
using BikeTrips.Data.Migrations;
using BikeTrips.Web.App_Start;
using BikeTrips.Web.Infrastructure.Mappings;
using System.Data.Entity;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BikeTrips.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BikeTripsDbContext, Configuration>());
            AutofacConfig.RegisterAutofac();
            AutoMapperConfig automapperConfig = new AutoMapperConfig();
            automapperConfig.Execute(Assembly.GetExecutingAssembly());


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
