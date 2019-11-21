using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //   name:"null",
            //    url:"{controller}/{action}/{id}",
            //    new {controller = "Home", action = "Index", id= UrlParameter.Optional}
            //);
            //Thay đổi Controller và Action để không bị dò

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Wrong",
                url: "Home/Index",
                defaults: new { controller = "Home", action = "AnUong", id = UrlParameter.Optional }
            );
        }
    }
}
