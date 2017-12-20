using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TEB.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var tebData = Assembly.Load("TEB.Data");
            Type repository = tebData.GetTypes().Where(x => x.Name == "GenericRepository`1").SingleOrDefault();
            Type irepository = tebData.GetTypes().Where(x => x.Name == "IGenericRepository`1").SingleOrDefault();

            builder.RegisterGeneric(repository).As(irepository).InstancePerRequest();

            builder.RegisterAssemblyTypes(Assembly.Load("TEB.Service"))
               .Where(t => t.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerRequest();

            var container = builder.Build();

            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }
    }
}
