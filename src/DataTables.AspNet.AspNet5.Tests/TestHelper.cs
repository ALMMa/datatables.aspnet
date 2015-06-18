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
using Microsoft.AspNet.Mvc.ModelBinding;

namespace DataTables.AspNet.AspNet5.Tests
{
    /// <summary>
    /// Provides arrange methods and helpers to execute unit tests.
    /// </summary>
    public static class TestHelper
    {
        public static Core.IColumn MockColumn(string columnName, string columnField, bool searchable, bool sortable)
        { return new Column(columnName, columnField, searchable, sortable, null); }
        public static Core.IColumn MockColumn(string columnName, string columnField, bool searchable, bool sortable, string searchValue, bool searchRegex)
        { return new Column(columnName, columnField, searchable, sortable, new Search(searchValue, searchRegex)); }
        public static Core.IOptions MockOptions()
        { return new Options(); }
        public static ModelBinder MockModelBinder()
        { return new ModelBinder(); }
        public static Core.IDataTablesRequest MockDataTablesRequest(int draw, int start, int length, Core.ISearch search, IEnumerable<Core.IColumn> columns)
        { return MockDataTablesRequest(draw, start, length, search, columns, null); }
        public static Core.IDataTablesRequest MockDataTablesRequest(int draw, int start, int length, Core.ISearch search, IEnumerable<Core.IColumn> columns, IDictionary<string, object> aditionalParameters)
        { return new DataTablesRequest(draw, start, length, search, columns, aditionalParameters); }
        public static System.Collections.IEnumerable MockData()
        { return new string[] { "firstElement", "secondElement", "thirdElement" }; }
        public static IDictionary<string, object> MockAditionalParameters()
        { return new Dictionary<string, object>() { { "firstParameter", "firstValue" }, { "secondParameter", 7 } }; }
        public static Core.ISearch MockSearch(string searchValue, bool isRegex)
        { return MockSearch(searchValue, isRegex, null); }
        public static Core.ISearch MockSearch(string searchValue, bool isRegex, string field)
        { return new Search(searchValue, isRegex, field); }
        public static Core.ISort MockSort(string field, int order, string direction)
        { return new Sort(field, order, direction); }
        public static IEnumerable<Core.IColumn> MockColumns()
        { 
            return new Column[]
            {
                new Column("column0_name", "column0_field", true, true, MockSearch("column0_search_value", true, "column0_field")),
                new Column("column1_name", "column1_field", true, false, MockSearch("column1_search_value", false, "column1_field")),
                new Column("column2_name", "column2_field", false, true, MockSearch("column2_search_value", true, "column2_field")),
            };
        }
        public static ModelBindingContext MockModelBindingContextWithCamelCase(string draw, string length, string start, string searchValue, string searchRegex)
        { return MockModelBindingContextWithCamelCase(draw, length, start, searchValue, searchRegex, null); }
        public static ModelBindingContext MockModelBindingContextWithCamelCase(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> aditionalParameters)
        { return MockModelBindingContext(draw, length, start, searchValue, searchRegex, aditionalParameters, new NameConvention.CamelCaseRequestNameConvention()); }
        public static ModelBindingContext MockModelBindingContextWithHungarianNotation(string draw, string length, string start, string searchValue, string searchRegex)
        { return MockModelBindingContextWithHungarianNotation(draw, length, start, searchValue, searchRegex, null); }
        public static ModelBindingContext MockModelBindingContextWithHungarianNotation(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> aditionalParameters)
        { return MockModelBindingContext(draw, length, start, searchValue, searchRegex, aditionalParameters, new NameConvention.HungarianNotationRequestNameConvention()); }
        public static ModelBindingContext MockModelBindingContext(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> aditionalParameters, Core.NameConvention.IRequestNameConvention requestNameConvention)
        {
            // Request properties.
            var formCollection = new Dictionary<string, object>()
            {
                { requestNameConvention.Length, length },
                { requestNameConvention.Start, start },
                { requestNameConvention.SearchValue, searchValue },
                { requestNameConvention.IsSearchRegex, searchRegex }
            };
            if (!String.IsNullOrWhiteSpace(draw)) formCollection.Add(requestNameConvention.Draw, draw);

            // Aditional parameters.
            if (aditionalParameters != null)
            {
                foreach (var keypair in aditionalParameters)
                {
                    formCollection.Add(keypair.Key, Convert.ToString(keypair.Value));
                }
            }

            // Value provider for request properties.
            var valueProvider = new DictionaryBasedValueProvider(new BindingSource("a", "a", false, true), formCollection);

            // Model metadata.
            var x = new Microsoft.AspNet.Mvc.ModelBinding.Metadata.DefaultCompositeMetadataDetailsProvider(null);
            var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider(x).GetMetadataForType(typeof(Core.IDataTablesRequest));
            //var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata. ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Core.IDataTablesRequest));

            return new ModelBindingContext()
            {
                ModelName = "moq",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };
        }        
        public static ModelBindingContext MockModelBindingContextWithInvalidRequest()
        {
            // Request properties.
            var formCollection = new Dictionary<string, object>();

            // Value provider for request properties.
            var valueProvider = new DictionaryBasedValueProvider(new BindingSource("a", "a", false, true), formCollection);

            // Model metadata.
            var x = new Microsoft.AspNet.Mvc.ModelBinding.Metadata.DefaultCompositeMetadataDetailsProvider(null);
            var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider(x).GetMetadataForType(typeof(Core.IDataTablesRequest));
            //var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata. ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Core.IDataTablesRequest));

            return new ModelBindingContext()
            {
                ModelName = "moq",
                ValueProvider = valueProvider,
                ModelMetadata = modelMetadata
            };
        }
        public static IDictionary<string, object> ParseAditionalParameters(ModelBindingContext modelBindingContext)
        {
            var _return = new Dictionary<string, object>();

            var firstParameter = modelBindingContext.ValueProvider.GetValueAsync("firstParameter").Result;
            _return.Add("firstParameter", firstParameter.AttemptedValue);

            var secondParameter = modelBindingContext.ValueProvider.GetValueAsync("secondParameter").Result;
            _return.Add("secondParameter", Convert.ToInt32(secondParameter.AttemptedValue));

            return _return;
        }
    }
}
