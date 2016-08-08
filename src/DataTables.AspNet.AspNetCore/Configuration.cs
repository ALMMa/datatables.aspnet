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
using System.IO;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;

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
        /// <param name="parseRequestAdditionalParameters">Function to evaluante and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="parseResponseAdditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void RegisterDataTables(this IServiceCollection services, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool parseResponseAdditionalParameters) { services.RegisterDataTables(new Options(), parseRequestAdditionalParameters, parseResponseAdditionalParameters); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="options">DataTables.AspNet options.</param>
        public static void RegisterDataTables(this IServiceCollection services, IOptions options) { services.RegisterDataTables(options, null, false); }

        /// <summary>
        /// Provides DataTables.AspNet registration for AspNet5 projects.
        /// </summary>
        /// <param name="services">Service collection for dependency injection.</param>
        /// <param name="options">DataTables.AspNet options.</param>
        /// <param name="parseRequestAdditionalParameters">Function to evaluate and parse aditional parameters sent within the request (user-defined parameters).</param>
        /// <param name="enableResponseAdditionalParameters">Indicates whether response aditional parameters parsing is enabled or not.</param>
        public static void RegisterDataTables(this IServiceCollection services, IOptions options, Func<ModelBindingContext, IDictionary<string, object>> parseRequestAdditionalParameters, bool enableResponseAdditionalParameters)
        {
            if (options == null) throw new ArgumentNullException(nameof(options), "Options for DataTables.AspNet cannot be null.");

            Options = options;

            services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(mvcOptions =>
            {
                var readerFactory = services.BuildServiceProvider().GetRequiredService<IHttpRequestStreamReaderFactory>();
                var formatter = mvcOptions.InputFormatters.Cast<JsonInputFormatter>().FirstOrDefault();
                var modelBinder = new ModelBinder(formatter, readerFactory);

                if (parseRequestAdditionalParameters != null)
                {
                    Options.EnableRequestAdditionalParameters();
                    modelBinder.ParseAdditionalParameters = parseRequestAdditionalParameters;
                }

                if (enableResponseAdditionalParameters)
                    Options.EnableResponseAdditionalParameters();
                
                // Should be inserted into first position because there is a generic binder which could end up resolving/binding model incorrectly.
                mvcOptions.ModelBinderProviders.Insert(0, new ModelBinderProvider(modelBinder, 
                    mvcOptions.InputFormatters.Cast<JsonInputFormatter>().FirstOrDefault(),
                    readerFactory));
            });
        }


        internal class ModelBinderProvider : IModelBinderProvider
        {
            public IModelBinder ModelBinder { get; private set; }
            private readonly JsonInputFormatter _formatter;
            private readonly IHttpRequestStreamReaderFactory _readerFactory;

            public ModelBinderProvider(ModelBinder binder, JsonInputFormatter formatter, IHttpRequestStreamReaderFactory readerFactory)
            {
                if (formatter == null)
                {
                    throw new ArgumentNullException(nameof(formatter));
                }

                if (readerFactory == null)
                {
                    throw new ArgumentNullException(nameof(readerFactory));
                }

                _formatter = formatter;
                _readerFactory = readerFactory;
                ModelBinder = binder;
            }

            public IModelBinder GetBinder(ModelBinderProviderContext context)
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                if (IsBindable(context.Metadata.ModelType))
                {
                    return ModelBinder ?? (ModelBinder = new ModelBinder(_formatter, _readerFactory));
                }
                else return null;
            }

            private bool IsBindable(Type type) { return type == typeof(IDataTablesRequest); }
        }
    }
}
