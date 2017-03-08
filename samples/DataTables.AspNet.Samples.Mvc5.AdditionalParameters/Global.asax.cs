#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos (Brazil)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion Copyright

using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace DataTables.AspNet.Samples.Mvc5.BasicIntegration
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // DataTables.AspNet registration with default options.
            var options = new DataTables.AspNet.Mvc5.Options()
                .EnableRequestAdditionalParameters()
                .EnableResponseAdditionalParameters();

            var binder = new DataTables.AspNet.Mvc5.ModelBinder();
            binder.ParseAdditionalParameters = Parser;

            DataTables.AspNet.Mvc5.Configuration.RegisterDataTables(options, binder);
        }


        private IDictionary<string, object> Parser(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            var p1 = System.Convert.ToString(modelBindingContext.ValueProvider.GetValue("param1").AttemptedValue);
            var p2 = System.Convert.ToInt32(modelBindingContext.ValueProvider.GetValue("param2").AttemptedValue);

            return new Dictionary<string, object>()
            {
                { "param1", p1 },
                { "param2", p2 }
            };
        }
    }
}
