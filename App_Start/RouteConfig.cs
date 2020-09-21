using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VidlyProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();  //to enable attribute routing

            //order of routes matter
            //must be defined from most specific to most generic

            routes.MapRoute(
                name: "MoviesByReleaseDate",
                url: "movies/released/{year}/{month}", //in curly braces as they are the optional parameters
                defaults: new { controller = "Movies", action = "ByReleaseDate" },
                new { year = @"\d{4}", month = @"\d{2}" }  //constraints on our parameters year and month \d is digits, {4} & {2} is length of values   
                                                           //@"2015|2016" means we allow only values 2015 and 2016
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",  //default parameter name is id
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
