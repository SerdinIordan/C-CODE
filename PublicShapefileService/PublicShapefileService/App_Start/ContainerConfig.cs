using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicShapefileService.Helpers;
using PublicShapefileService.BusinessLogic.Manager;
using PublicShapefileService.BusinessLogic.Interfaces;

using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

namespace PublicShapefileService.App_Start
{
    // Configuration of the Autofac IoC container
    public class ContainerConfig
    {
        public static void RegisterTypes()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DownloadHelper>().As<IDownloadHelper>().InstancePerHttpRequest();
            builder.RegisterType<Manager>().As<IManager>().InstancePerHttpRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();

            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);

            builder.RegisterModule<AutofacWebTypesModule>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}