﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace APIDatVe
{
    public static class WebApiConfig4
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
