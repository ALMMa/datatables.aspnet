#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos

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
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DataTables.Mvc
{
    /// <summary>
    /// Defines a DataTables binder to bind a model with the request parameters from DataTables.
    /// </summary>
    public class DataTablesBinder : IModelBinder
    {
        const string COLUMN_DATA_FORMATTING = "columns[{0}][data]";
        const string COLUMN_NAME_FORMATTING = "columns[{0}][name]";
        const string COLUMN_SEARCHABLE_FORMATTING = "columns[{0}][searchable]";
        const string COLUMN_ORDERABLE_FORMATTING = "columns[{0}][orderable]";
        const string COLUMN_SEARCH_VALUE_FORMATTING = "columns[{0}][search][value]";
        const string COLUMN_SEARCH_REGEX_FORMATTING = "columns[{0}][search][regex]";
        const string ORDER_COLUMN_FORMATTING = "order[{0}][column]";
        const string ORDER_DIRECTION_FORMATTING = "order[{0}][dir]";
        /// <summary>
        /// Binds a new model with the DataTables request parameters.
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = new DataTablesRequest();

            var request = controllerContext.RequestContext.HttpContext.Request;
            var requestMethod = request.HttpMethod.ToLower();
            
            NameValueCollection requestParameters = null;
            if (requestMethod.Equals("post")) requestParameters = request.Form;
            else if (requestMethod.Equals("get")) requestParameters = request.QueryString;
            else throw new ArgumentException(String.Format("The provided HTTP method ({0}) is not a valid method to use with DataTablesBinder. Please, use HTTP GET or POST methods only.", requestMethod), "method");

            // Populates the model with the draw count from DataTables.
            model.Draw = requestParameters.G<int>("draw");
            
            // Populates the model with page info (server-side paging).
            model.Start = requestParameters.G<int>("start");
            model.Length = requestParameters.G<int>("length");

            // Populates the model with search (global search).
            var searchValue = requestParameters.G<string>("search[value]");
            var searchRegex = requestParameters.G<bool>("search[regex]");
            model.Search = new Search(searchValue, searchRegex);

            // Loop through every request parameter to avoid missing any DataTable column.
            for (int i = 0; i < requestParameters.Count; i++)
            {
                var columnData = requestParameters.G<string>(String.Format(COLUMN_DATA_FORMATTING, i));
                var columnName = requestParameters.G<string>(String.Format(COLUMN_NAME_FORMATTING, i));

                if (columnData != null && columnName != null)
                {
                    var columnSearchable = requestParameters.G<bool>(String.Format(COLUMN_SEARCHABLE_FORMATTING, i));
                    var columnOrderable = requestParameters.G<bool>(String.Format(COLUMN_ORDERABLE_FORMATTING, i));
                    var columnSearchValue = requestParameters.G<string>(String.Format(COLUMN_SEARCH_VALUE_FORMATTING, i));
                    var columnSearchRegex = requestParameters.G<bool>(String.Format(COLUMN_SEARCH_REGEX_FORMATTING, i));

                    var column = new Column(
                        columnData,
                        columnName,
                        columnSearchable,
                        columnOrderable,
                        columnSearchValue,
                        columnSearchRegex);

                    model.Columns.Add(column);
                }
                else break; // Stops iterating because there's no more columns.
            }

            // Configure column's ordering.
            for (int i = 0; i < requestParameters.Count; i++)
            {
                var orderColumn = requestParameters.G<int>(String.Format(ORDER_COLUMN_FORMATTING, i));
                var orderDirection = requestParameters.G<string>(String.Format(ORDER_DIRECTION_FORMATTING, i));

                if (orderColumn != null && orderDirection != null)
                    model.Columns.ElementAt(orderColumn).SetColumnOrdering(i, orderDirection);
            }

            // Casts the model into it's interface to protect against changes.
            return (IDataTablesRequest)model;
        }
    }
}
