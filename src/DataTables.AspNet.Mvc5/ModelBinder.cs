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

using DataTables.AspNet.Core;
using DataTables.AspNet.Core.NameConvention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DataTables.AspNet.Mvc5
{
    /// <summary>
    /// Represents a model binder for DataTables request element.
    /// </summary>
    public class ModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="controllerContext">Controller context for execution.</param>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BindModel(
                controllerContext,
                bindingContext,
                DataTables.AspNet.Mvc5.Configuration.Options,
                ParseAdditionalParameters);
        }

        /// <summary>
        /// For internal and testing use only.
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="controllerContext">Controller context for execution.</param>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <param name="options">DataTables.AspNet global options.</param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext, IOptions options, Func<ControllerContext, ModelBindingContext, IDictionary<string, object>> parseAdditionalParameters)
        {
            if (options == null || options.RequestNameConvention == null) return null;

            var values = bindingContext.ValueProvider;

            // Accordingly to DataTables docs, it is recommended to receive/return draw casted as int for security reasons.
            // This is meant to help prevent XSS attacks.
            var draw = values.GetValue(options.RequestNameConvention.Draw);
            int _draw = 0;
            if (options.IsDrawValidationEnabled && !Parse<Int32>(draw, out _draw))
                return null;

            var start = values.GetValue(options.RequestNameConvention.Start);
            Parse<int>(start, out int _start);

            var length = values.GetValue(options.RequestNameConvention.Length);
            int _length = options.DefaultPageLength;
            Parse<int>(length, out _length);

            var searchValue = values.GetValue(options.RequestNameConvention.SearchValue);
            Parse<string>(searchValue, out string _searchValue);

            var searchRegex = values.GetValue(options.RequestNameConvention.IsSearchRegex);
            Parse<bool>(searchRegex, out bool _searchRegex);

            var search = new Search(_searchValue, _searchRegex);

            // Parse columns & column sorting.
            var columns = ParseColumns(values, options.RequestNameConvention);
            var sorting = ParseSorting(columns, values, options.RequestNameConvention);

            if (options.IsRequestAdditionalParametersEnabled && parseAdditionalParameters != null)
            {
                var additionalParameters = parseAdditionalParameters(controllerContext, bindingContext);
                return new DataTablesRequest(_draw, _start, _length, search, columns, additionalParameters);
            }
            else
            {
                return new DataTablesRequest(_draw, _start, _length, search, columns);
            }
        }

        /// <summary>
        /// Provides custom aditional parameters processing for your request.
        /// You have to implement this to populate 'IDataTablesRequest' object with aditional (user-defined) request values.
        /// </summary>
        public Func<ControllerContext, ModelBindingContext, IDictionary<string, object>> ParseAdditionalParameters;

        /// <summary>
        /// For internal use only.
        /// Parse column collection.
        /// </summary>
        /// <param name="values">Request parameters.</param>
        /// <param name="names">Name convention for request parameters.</param>
        /// <returns></returns>
        private static IEnumerable<IColumn> ParseColumns(IValueProvider values, IRequestNameConvention names)
        {
            var columns = new List<IColumn>();

            int counter = 0;
            while (true)
            {
                // Parses Field value.
                var columnField = values.GetValue(String.Format(names.ColumnField, counter));
                if (!Parse<string>(columnField, out string _columnField)) break;

                // Parses Name value.
                var columnName = values.GetValue(String.Format(names.ColumnName, counter));
                if (!Parse<string>(columnName, out string _columnName)) break;

                // Parses Orderable value.
                var columnSortable = values.GetValue(String.Format(names.IsColumnSortable, counter));
                Parse<bool>(columnSortable, out bool _columnSortable);

                // Parses Searchable value.
                var columnSearchable = values.GetValue(String.Format(names.IsColumnSearchable, counter));
                Parse<bool>(columnSearchable, out bool _columnSearchable);

                // Parsed Search value.
                var columnSearchValue = values.GetValue(String.Format(names.ColumnSearchValue, counter));
                Parse<string>(columnSearchValue, out string _columnSearchValue);

                // Parses IsRegex value.
                var columnSearchRegex = values.GetValue(String.Format(names.IsColumnSearchRegex, counter));
                Parse<bool>(columnSearchRegex, out bool _columnSearchRegex);

                var search = new Search(_columnSearchValue, _columnSearchRegex, _columnField);

                // Instantiates a new column with parsed elements.
                var column = new Column(_columnName, _columnField, _columnSearchable, _columnSortable, search);

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
        /// <param name="values">Request parameters.</param>
        /// <param name="names">Name convention for request parameters.</param>
        /// <returns></returns>
        private static IEnumerable<ISort> ParseSorting(IEnumerable<IColumn> columns, IValueProvider values, IRequestNameConvention names)
        {
            var sorting = new List<ISort>();

            for (int i = 0; i < columns.Count(); i++)
            {
                var sortField = values.GetValue(String.Format(names.SortField, i));
                if (!Parse<int>(sortField, out int _sortField)) break;

                var column = columns.ElementAt(_sortField);

                var sortDirection = values.GetValue(String.Format(names.SortDirection, i));
                Parse<string>(sortDirection, out string _sortDirection);

                if (column.SetSort(i, _sortDirection))
                    sorting.Add(column.Sort);
            }

            return sorting;
        }

        /// <summary>
        /// Parses a possible raw value and transforms into a strongly-typed result.
        /// </summary>
        /// <typeparam name="ElementType">The expected type for result.</typeparam>
        /// <param name="value">The possible request value.</param>
        /// <param name="result">Returns the parsing result or default value for type is parsing failed.</param>
        /// <returns>True if parsing succeeded, False otherwise.</returns>
        private static bool Parse<ElementType>(ValueProviderResult value, out ElementType result)
        {
            result = default;

            if (value == null) return false;
            if (value.RawValue == null) return false;

            try
            {
                result = (ElementType)Convert.ChangeType(value.AttemptedValue, typeof(ElementType));
                return true;
            }
            catch { return false; }
        }
    }
}