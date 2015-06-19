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
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Framework.DependencyInjection;

namespace DataTables.AspNet.AspNet5
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
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        public static void UseDataTables(this IServiceCollection services) { services.UseDataTables(new Options()); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="options">DataTables.AspNet options.</param>
        public static void UseDataTables(this IServiceCollection services, IOptions options) { services.UseDataTables(options, new ModelBinder()); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
        public static void UseDataTables(this IServiceCollection services, ModelBinder requestModelBinder) { services.UseDataTables(new Options(), requestModelBinder); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="parseRequestAditionalParameters">Function to evaluante and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="parseResponseAditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void UseDataTables(this IServiceCollection services, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAditionalParameters, bool parseResponseAditionalParameters) { services.UseDataTables(new Options(), new ModelBinder(), parseRequestAditionalParameters, parseResponseAditionalParameters); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="options">DataTables.AspNet options.</param>
        /// <param name="requestModelBinder">Model binder to use when resolving 'IDataTablesRequest' model.</param>
        public static void UseDataTables(this IServiceCollection services, IOptions options, ModelBinder requestModelBinder) { services.UseDataTables(options, requestModelBinder, null, false); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="options">DataTables.AspNet options.</param>
        /// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
        /// <param name="parseRequestAditionalParameters">Function to evaluate and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="enableResponseAditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void UseDataTables(this IServiceCollection services, IOptions options, ModelBinder requestModelBinder, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAditionalParameters, bool enableResponseAditionalParameters)
        {
            if (options == null) throw new ArgumentNullException("options", "Options for DataTables.AspNet cannot be null.");
            if (requestModelBinder == null) throw new ArgumentNullException("requestModelBinder", "Request model binder for DataTables.AspNet cannot be null.");

            Options = options;

            if (parseRequestAditionalParameters != null)
            {
                Options.EnableRequestAditionalParameters();
                requestModelBinder.ParseAditionalParameters = parseRequestAditionalParameters;
            }

            if (enableResponseAditionalParameters)
                Options.EnableResponseAditionalParameters();

            services.Configure<Microsoft.AspNet.Mvc.MvcOptions>(_options =>
            {
                _options.ModelBinders.Add(new Microsoft.AspNet.Mvc.OptionDescriptors.ModelBinderDescriptor(requestModelBinder));
            });            
        }
    }
}
