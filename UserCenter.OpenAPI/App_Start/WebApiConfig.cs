using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using UserCenter.OpenAPI.Filter;

namespace UserCenter.OpenAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Services.Replace(typeof(IHttpControllerSelector),
            new VersionControllerSelector(config));
            //一个对象必须是Ioc创建出来的，Ioc容器才会帮我们注入
            // config.Filters.Add(new  UCAuthorFilter());
            // UCAuthorFilter authorFilter = (UCAuthorFilter)config.Services.GetService(typeof(UCAuthorFilter));
            UCAuthorFilter authorFilter = (UCAuthorFilter)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(UCAuthorFilter));
            config.Filters.Add(authorFilter);
        }
    }
}
