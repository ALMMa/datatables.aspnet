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

using System;
using System.Collections.Generic;
using DataTables.AspNet.Core;
using System.Web.Http.ModelBinding;
using System.Web.Http.Controllers;
using System.Web.Http.ValueProviders;
using System.Web.Http;

namespace DataTables.AspNet.WebApi2
{
    /// <summary>
    /// Handles DataTables.AspNet registration and holds default (global) configuration options.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Get's DataTables.AspNet runtime options for server-side processing.
        /// </summary>
        public static IOptions Options { get; private set; }

        /// <summary>
        /// Static constructor.
        /// Set's default configuration for DataTables.AspNet.
        /// </summary>
        static Configuration()
        {
            Options = new Options();
        }

        /// <summary>
        /// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
        /// </summary>
        public static void RegisterDataTables(this HttpConfiguration configuration) { RegisterDataTables(configuration, new Options()); }

		/// <summary>
		/// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
		/// </summary>
		/// <param name="options">DataTables.AspNet options.</param>
		public static void RegisterDataTables(this HttpConfiguration configuration, IOptions options) { RegisterDataTables(configuration, options, new ModelBinder()); }

		/// <summary>
		/// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
		/// </summary>
		/// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
		public static void RegisterDataTables(this HttpConfiguration configuration, ModelBinder requestModelBinder) { RegisterDataTables(configuration, new Options(), requestModelBinder); }

		/// <summary>
		/// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
		/// </summary>
		/// <param name="parseRequestAdditionalParameters">Function to evaluante and parse aditional parameters sent within the request (user-defined parameters).</param>
		/// <param name="parseResponseAdditionalParameters">Indicates if additional parameters will be sent with the response.</param>
		public static void RegisterDataTables(this HttpConfiguration configuration, Func<HttpActionContext, ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool parseResponseAdditionalParameters) { RegisterDataTables(configuration, new Options(), new ModelBinder(), parseRequestAdditionalParameters, parseResponseAdditionalParameters); }

		/// <summary>
		/// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
		/// </summary>
		/// <param name="options">DataTables.AspNet options.</param>
		/// <param name="requestModelBinder">Model binder to use when resolving 'IDataTablesRequest' model.</param>
		public static void RegisterDataTables(this HttpConfiguration configuration, IOptions options, ModelBinder requestModelBinder) { RegisterDataTables(configuration, options, requestModelBinder, null, false); }

		/// <summary>
		/// Provides DataTables.AspNet registration for Asp.Net WebApi 2 projects.
		/// </summary>
		/// <param name="options">DataTables.AspNet options.</param>
		/// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
		/// <param name="parseRequestAdditionalParameters">Function to evaluate and parse aditional parameters sent within the request (user-defined parameters).</param>
		/// <param name="parseResponseAdditionalParameters">Indicates if additional parameters are sent with the response.</param>
		public static void RegisterDataTables(this HttpConfiguration configuration, IOptions options, ModelBinder requestModelBinder, Func<HttpActionContext, ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool parseResponseAdditionalParameters)
        {
            if (options == null) throw new ArgumentNullException("options", "Options for DataTables.AspNet cannot be null.");
            if (requestModelBinder == null) throw new ArgumentNullException("requestModelBinder", "Request model binder for DataTables.AspNet cannot be null.");

            Options = options;

			configuration.BindParameter(typeof(IDataTablesRequest), requestModelBinder);

			if (parseRequestAdditionalParameters != null)
            {
                Options.EnableRequestAdditionalParameters();
                requestModelBinder.ParseAdditionalParameters = parseRequestAdditionalParameters;
            }

            if (parseResponseAdditionalParameters)
                Options.EnableResponseAdditionalParameters();
        }
    }
}
