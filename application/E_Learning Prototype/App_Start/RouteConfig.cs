using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace E_Learning_Prototype
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Code",
                url: "Stream/index/{Code}",
                defaults: new { controller = "Stream", action = "Index" }

            );

            routes.MapRoute(
                name: "Code4Devoire",
                url: "Devoir/{action}/{Code}",
                defaults: new { controller = "Devoir", action = "Index" }

            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Course", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
