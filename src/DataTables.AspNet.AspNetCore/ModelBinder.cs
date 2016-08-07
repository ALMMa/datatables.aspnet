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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using DataTables.AspNet.Core.NameConvention;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// Represents a model binder for DataTables request element.
    /// </summary>
    public class ModelBinder : IModelBinder
    {
        private readonly JsonInputFormatter _formatter;
        private readonly Func<Stream, Encoding, TextReader> _readerFactory;
        public ModelBinder(JsonInputFormatter formatter, IHttpRequestStreamReaderFactory readerFactory)
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
            _readerFactory = readerFactory.CreateReader;
        }
        /// <summary>
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            //BindModel(
            //       bindingContext,
            //       DataTables.AspNet.AspNetCore.Configuration.Options,
            //       ParseAdditionalParameters);

            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Model binding is not set, thus AspNet5 will keep looking for other model binders.
            if (bindingContext.ModelType != typeof(IDataTablesRequest))
            {
                //return ModelBindingResult.NoResult;
                return Task.FromResult(0);
            }

            var options = Configuration.Options;

            // Binding is set to a null model to avoid unexpected errors.
            if (options == null || options.RequestNameConvention == null)
            {
                //return ModelBindingResult.Failed(bindingContext.ModelName);
                bindingContext.Result = ModelBindingResult.Failed(/*bindingContext.ModelName*/);
                return Task.FromResult(0);
            }

            // Special logic for body, treat the model name as string.Empty for the top level
            // object, but allow an override via BinderModelName. The purpose of this is to try
            // and be similar to the behavior for POCOs bound via traditional model binding.
            string modelBindingKey;
            if (bindingContext.IsTopLevelObject)
            {
                modelBindingKey = bindingContext.BinderModelName ?? string.Empty;
            }
            else
            {
                modelBindingKey = bindingContext.ModelName;
            }

            try
            {
                JObject jsonModel;
                var request = bindingContext.HttpContext.Request;
                using (var streamReader = _readerFactory(request.Body, MediaType.GetEncoding(request.ContentType)))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        jsonModel = (JObject)new JsonSerializer().Deserialize(jsonReader);
                    }
                }

                // Accordingly to DataTables docs, it is recommended to receive/return draw casted as int for security reasons.
                // This is meant to help prevent XSS attacks.
                var draw = (int?)jsonModel[options.RequestNameConvention.Draw] ?? 0;
                if (options.IsDrawValidationEnabled && draw == 0)
                {
                    bindingContext.Result = ModelBindingResult.Failed();
                    return Task.FromResult(0);
                }

                var start = (int?)jsonModel[options.RequestNameConvention.Start] ?? 0;
                var length = (int?)jsonModel[options.RequestNameConvention.Length] ?? options.DefaultPageLength;
                var searchValue = (string)jsonModel.SelectToken(options.RequestNameConvention.SearchValue);
                var searchRegex = (bool?)jsonModel.SelectToken(options.RequestNameConvention.IsSearchRegex) ?? false;
                
                var search = new Search(searchValue, searchRegex);

                // Parse columns & column sorting.
                var columns = ParseColumns(jsonModel, options.RequestNameConvention);
                var sorting = ParseSorting(columns, jsonModel, options.RequestNameConvention);

                if (options.IsRequestAdditionalParametersEnabled && ParseAdditionalParameters != null)
                {
                    var aditionalParameters = ParseAdditionalParameters(bindingContext);
                    var model = new DataTablesRequest(draw, start, length, search, columns, sorting, aditionalParameters);
                    {
                        //return ModelBindingResult.Success(bindingContext.ModelName, model);
                        bindingContext.Result = ModelBindingResult.Success(/*bindingContext.ModelName,*/ model);
                        return Task.FromResult(0);
                    }
                }
                else
                {
                    var model = new DataTablesRequest(draw, start, length, search, columns, sorting);
                    {
                        //return ModelBindingResult.Success(bindingContext.ModelName, model);
                        bindingContext.Result = ModelBindingResult.Success(/*bindingContext.ModelName,*/ model);
                        return Task.FromResult(0);
                    }
                }
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError(modelBindingKey, ex, bindingContext.ModelMetadata);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// For internal and testing use only.
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <param name="options">DataTables.AspNet global options.</param>
        /// <param name="parseAditionalParameters"></param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public virtual void BindModel(ModelBindingContext bindingContext, IOptions options, Func<ModelBindingContext, IDictionary<string, object>> parseAditionalParameters)
        {
            //using for test purpose only
            //todo: call async method
        }

        /// <summary>
        /// Provides custom aditional parameters processing for your request.
        /// You have to implement this to populate 'IDataTablesRequest' object with aditional (user-defined) request values.
        /// </summary>
        public Func<ModelBindingContext, IDictionary<string, object>> ParseAdditionalParameters;

        /// <summary>
        /// For internal use only.
        /// Parse column collection.
        /// </summary>
        /// <param name="jsonModel">Request parameters.</param>
        /// <param name="names">Name convention for request parameters.</param>
        /// <returns></returns>
        private static List<IColumn> ParseColumns(JObject jsonModel, IRequestNameConvention names)
        {
            var columns = new List<IColumn>();

            var counter = 0;
            while (true)
            {
                // Parses Field value.
                var columnField = (string) jsonModel.SelectToken(string.Format(names.ColumnField, counter));
                if (columnField == null) break;
                // Parses Name value.
                var columnName = (string) jsonModel.SelectToken(string.Format(names.ColumnName, counter));
                if (columnName == null) break;
                // Parses Orderable value.
                var columnSortable = (bool?) jsonModel.SelectToken(string.Format(names.IsColumnSortable, counter)) ?? true;
                // Parses Searchable value.
                var columnSearchable = (bool?) jsonModel.SelectToken(string.Format(names.IsColumnSearchable, counter)) ?? true;
                // Parsed Search value.
                var columnSearchValue = (string) jsonModel.SelectToken(string.Format(names.ColumnSearchValue, counter));
                // Parses IsRegex value.
                var columnSearchRegex = (bool?) jsonModel.SelectToken(string.Format(names.IsColumnSearchRegex, counter)) ?? false;
                var search = new Search(columnSearchValue, columnSearchRegex);
                // Instantiates a new column with parsed elements.
                var column = new Column(columnName, columnField, columnSearchable, columnSortable, search);
                // Adds the column to the return collection.
                columns.Add(column);
                // Increments counter to keep processing columns.
                counter++;
            }

            return columns;
        }

        /// <summary>
        /// For internal use only.
        /// Parse sort collection.
        /// </summary>
        /// <param name="columns">Column collection to use when parsing sort.</param>
        /// <param name="jsonModel">Request parameters.</param>
        /// <param name="names">Name convention for request parameters.</param>
        /// <returns></returns>
        private static List<ISort> ParseSorting(List<IColumn> columns, JObject jsonModel, IRequestNameConvention names)
        {
            var sorting = new List<ISort>();

            for (var i = 0; i < columns.Count; i++)
            {
                var sortField = (int?) jsonModel.SelectToken(string.Format(names.SortField, i));
                if (sortField == null) break;
                var column = columns.ElementAt((int) sortField);

                var sortDirection = (string)jsonModel.SelectToken(string.Format(names.SortDirection, i));

                if (column.SetSort(i, sortDirection))
                    sorting.Add(column.Sort);
            }

            return sorting;
        }
    }
}
