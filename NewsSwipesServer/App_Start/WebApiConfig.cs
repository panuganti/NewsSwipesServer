﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NewsSwipesServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Remove XML formatter to enable Json output
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
