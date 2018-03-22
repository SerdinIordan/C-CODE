using PublicShapefileService.App_Start;
using PublicShapefileService.DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace PublicShapefileService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer<PublicShapefileServiceContext>(new CreateDatabaseIfNotExists<PublicShapefileServiceContext>());
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            CacheConfig.DoCaching(ConfigurationManager.AppSettings["route"]);
            ContainerConfig.RegisterTypes();
            AutoMapperWebConfiguration.RegisterMaps();
        }
    }
}