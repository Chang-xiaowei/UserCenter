using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using UserCenter.IServices;
using UserCenter.OpenAPI.Filter;

namespace UserCenter.OpenAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
       
        protected void Application_Start()
        {
            InitAutoFac();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
        private void InitAutoFac()
        {
            var configuration = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            // Register API controllers using assembly scanning.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            builder.RegisterWebApiFilterProvider(configuration);
            ////一个对象必须是Ioc创建出来的，Ioc容器才会帮我们注入
            builder.RegisterType(typeof(UCAuthorFilter)).PropertiesAutowired();

            var services = Assembly.Load("UserCenter.Services");
            builder.RegisterAssemblyTypes(services).Where(type => !type.IsAbstract && typeof(IServiceTag)
            .IsAssignableFrom(type)).AsImplementedInterfaces().SingleInstance().PropertiesAutowired();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}
