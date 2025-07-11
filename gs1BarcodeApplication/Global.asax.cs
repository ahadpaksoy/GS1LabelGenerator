
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using gs1BarcodeApplication.Services; 
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace gs1BarcodeApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            // --- SERILOG CONFIGURATION 
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext() // This is a standard enricher
                .WriteTo.File(
                    formatter: new CompactJsonFormatter(),
                    path: Server.MapPath("~/App_Data/Logs/log-.txt"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    // This helps prevent file locking issues
                    shared: true
                )
                .CreateLogger();

            // Log that the application is starting.
            Log.Information("Application Starting Up");
            // --- END OF SERILOG CONFIGURATION ---

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<WebConfigPresetService>().As<IPresetService>().InstancePerRequest();
            builder.RegisterType<Gs1DefinitionService>().As<IGs1DefinitionService>().SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        protected void Application_Error(object sender, EventArgs e)
        {            
            var exception = Server.GetLastError();
            if (exception != null)
            {              
                Log.Error(exception, "An unhandled exception occurred in the application.");
            }
        }

       
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Log.Information("HTTP {Method} {Url} received from {IpAddress}",
                Request.HttpMethod,
                Request.Url.PathAndQuery,
                Request.UserHostAddress);
        }

        protected void Application_End()
        {
            Log.Information("Application Shutting Down");
            Log.CloseAndFlush(); 
        }
    }
}