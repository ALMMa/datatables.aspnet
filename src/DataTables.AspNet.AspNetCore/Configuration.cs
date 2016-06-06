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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;

namespace DataTables.AspNet.AspNetCore
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
        public static void RegisterDataTables(this IServiceCollection services) { services.RegisterDataTables(new Options()); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="options">DataTables.AspNet options.</param>
        public static void RegisterDataTables(this IServiceCollection services, IOptions options) { services.RegisterDataTables(options, new ModelBinder()); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
        public static void RegisterDataTables(this IServiceCollection services, ModelBinder requestModelBinder) { services.RegisterDataTables(new Options(), requestModelBinder); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="parseRequestAdditionalParameters">Function to evaluante and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="parseResponseAdditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void RegisterDataTables(this IServiceCollection services, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool parseResponseAdditionalParameters) { services.RegisterDataTables(new Options(), new ModelBinder(), parseRequestAdditionalParameters, parseResponseAdditionalParameters); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="options">DataTables.AspNet options.</param>
        /// <param name="requestModelBinder">Model binder to use when resolving 'IDataTablesRequest' model.</param>
        public static void RegisterDataTables(this IServiceCollection services, IOptions options, ModelBinder requestModelBinder) { services.RegisterDataTables(options, requestModelBinder, null, false); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="options">DataTables.AspNet options.</param>
        /// <param name="requestModelBinder">Request model binder to use when resolving 'IDataTablesRequest' models.</param>
        /// <param name="parseRequestAdditionalParameters">Function to evaluate and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="enableResponseAdditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void RegisterDataTables(this IServiceCollection services, IOptions options, ModelBinder requestModelBinder, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool enableResponseAdditionalParameters)
        {
            if (options == null) throw new ArgumentNullException("options", "Options for DataTables.AspNet cannot be null.");
            if (requestModelBinder == null) throw new ArgumentNullException("requestModelBinder", "Request model binder for DataTables.AspNet cannot be null.");

            Options = options;

            if (parseRequestAdditionalParameters != null)
            {
                Options.EnableRequestAdditionalParameters();
                requestModelBinder.ParseAdditionalParameters = parseRequestAdditionalParameters;
            }

            if (enableResponseAdditionalParameters)
                Options.EnableResponseAdditionalParameters();

            services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(_options =>
            {
				// Should be inserted into first position because there is a generic binder which could end up resolving/binding model incorrectly.
                _options.ModelBinderProviders.Insert(0, new ModelBinderProvider(requestModelBinder));
            });
        }


        internal class ModelBinderProvider : IModelBinderProvider
        {
            public IModelBinder ModelBinder { get; private set; }
            public ModelBinderProvider() { }
            public ModelBinderProvider(IModelBinder modelBinder) { ModelBinder = modelBinder; }
            public IModelBinder GetBinder(ModelBinderProviderContext context)
            {
                if (IsBindable(context.Metadata.ModelType))
                {
                    if (ModelBinder == null) ModelBinder = new ModelBinder();
                    return ModelBinder;
                }
                else return null;
            }
            private bool IsBindable(Type type) { return type.Equals(typeof(DataTables.AspNet.Core.IDataTablesRequest)); }
        }
    }
}
