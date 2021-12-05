using DataTables.AspNet.Core;
using DataTables.AspNet.Core.NameConvention;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// Represents a model binder for DataTables request element.
    /// </summary>
    public class ModelBinder : IModelBinder
    {
        /// <summary>
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            return Task.Factory.StartNew(() =>
            {
                BindModel(
                    bindingContext,
                    DataTables.AspNet.AspNetCore.Configuration.Options,
                    ParseAdditionalParameters);
            });
        }
        /// <summary>
        /// For internal and testing use only.
        /// Binds request data/parameters/values into a 'IDataTablesRequest' element.
        /// </summary>
        /// <param name="controllerContext">Controller context for execution.</param>
        /// <param name="bindingContext">Binding context for data/parameters/values.</param>
        /// <param name="options">DataTables.AspNet global options.</param>
        /// <returns>An IDataTablesRequest object or null if binding was not possible.</returns>
        public virtual void BindModel(ModelBindingContext bindingContext, IOptions options, Func<ModelBindingContext, IDictionary<string, object>> parseAditionalParameters)
        {
            // Model binding is not set, thus AspNet5 will keep looking for other model binders.
            if (!bindingContext.ModelType.Equals(typeof(Core.IDataTablesRequest)))
            {
                //return ModelBindingResult.NoResult;
                return;
            }

            // Binding is set to a null model to avoid unexpected errors.
            if (options == null || options.RequestNameConvention == null)
            {
                //return ModelBindingResult.Failed(bindingContext.ModelName);
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var values = bindingContext.ValueProvider;

            // Accordingly to DataTables docs, it is recommended to receive/return draw casted as int for security reasons.
            // This is meant to help prevent XSS attacks.
            var draw = values.GetValue(options.RequestNameConvention.Draw);
            int _draw = 0;
            if (options.IsDrawValidationEnabled && !Parse<Int32>(draw, out _draw))
            {
                //return ModelBindingResult.Failed(bindingContext.ModelName); // Null model result (invalid request).
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            var start = values.GetValue(options.RequestNameConvention.Start);
            Parse<int>(start, out int _start);

            var length = values.GetValue(options.RequestNameConvention.Length);
            Parse<int>(length, out int _length);

            var searchValue = values.GetValue(options.RequestNameConvention.SearchValue);
            Parse<string>(searchValue, out string _searchValue);

            var searchRegex = values.GetValue(options.RequestNameConvention.IsSearchRegex);
            Parse<bool>(searchRegex, out bool _searchRegex);

            var search = new Search(_searchValue, _searchRegex);

            // Parse columns & column sorting.
            var columns = ParseColumns(values, options.RequestNameConvention);
            //var sorting = ParseSorting(columns, values, options.RequestNameConvention);

            if (options.IsRequestAdditionalParametersEnabled && parseAditionalParameters != null)
            {
                var aditionalParameters = parseAditionalParameters(bindingContext);
                var model = new DataTablesRequest(_draw, _start, _length, search, columns, aditionalParameters);
                {
                    //return ModelBindingResult.Success(bindingContext.ModelName, model);
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return;
                }
            }
            else
            {
                var model = new DataTablesRequest(_draw, _start, _length, search, columns);
                {
                    //return ModelBindingResult.Success(bindingContext.ModelName, model);
                    bindingContext.Result = ModelBindingResult.Success(model);
                    return;
                }
            }
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
                var columnField = values.GetValue(string.Format(names.ColumnField, counter));
                if (!Parse<string>(columnField, out string _columnField)) break;

                // Parses Name value.
                var columnName = values.GetValue(string.Format(names.ColumnName, counter));
                Parse<string>(columnName, out string _columnName);

                // Parses Orderable value.
                var columnSortable = values.GetValue(string.Format(names.IsColumnSortable, counter));
                Parse<bool>(columnSortable, out bool _columnSortable);

                // Parses Searchable value.
                var columnSearchable = values.GetValue(string.Format(names.IsColumnSearchable, counter));
                Parse<bool>(columnSearchable, out bool _columnSearchable);

                // Parsed Search value.
                var columnSearchValue = values.GetValue(string.Format(names.ColumnSearchValue, counter));
                Parse<string>(columnSearchValue, out string _columnSearchValue);

                // Parses IsRegex value.
                var columnSearchRegex = values.GetValue(string.Format(names.IsColumnSearchRegex, counter));
                Parse<bool>(columnSearchRegex, out bool _columnSearchRegex);

                var search = new Search(_columnSearchValue, _columnSearchRegex);

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
        //private static IEnumerable<ISort> ParseSorting(IEnumerable<IColumn> columns, IValueProvider values, IRequestNameConvention names)
        //{
        //    var sorting = new List<ISort>();

        //    for (int i = 0; i < columns.Count(); i++)
        //    {
        //        var sortField = values.GetValue(string.Format(names.SortField, i));
        //        if (!Parse<int>(sortField, out int _sortField)) break;

        //        var column = columns.ElementAt(_sortField);

        //        var sortDirection = values.GetValue(string.Format(names.SortDirection, i));
        //        Parse<string>(sortDirection, out string _sortDirection);

        //        if (column.SetSort(i, _sortDirection))
        //            sorting.Add(column.Sort);
        //    }

        //    return sorting;
        //}

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

            if (string.IsNullOrWhiteSpace(value.FirstValue)) return false;

            try
            {
                result = (ElementType)Convert.ChangeType(value.FirstValue, typeof(ElementType));
                return true;
            }
            catch { return false; }
        }
    }
}
