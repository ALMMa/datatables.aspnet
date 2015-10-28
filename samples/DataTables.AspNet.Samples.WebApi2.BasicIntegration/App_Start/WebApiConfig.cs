using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using DataTables.AspNet.Core;
using DataTables.AspNet.WebApi2;
using System.Web.Http.ModelBinding.Binders;
using System.Web.Http.ModelBinding;

namespace DataTables.AspNet.Samples.WebApi2.BasicIntegration
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			// DataTables.AspNet registration with default options.
			config.RegisterDataTables();
			
			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
		}
    }
}
