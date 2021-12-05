using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using DataTables.AspNet.Core;
using Moq;
using DataTables.AspNet.AspNetCore.Tests.Mocks;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Provides arrange methods and helpers to execute unit tests.
    /// </summary>
    public static class TestHelper
    {
        public static IColumn MockColumn(string columnName, string columnField, bool searchable, bool sortable)
        { return new Column(columnName, columnField, searchable, sortable, null); }
        public static IColumn MockColumn(string columnName, string columnField, bool searchable, bool sortable, string searchValue, bool searchRegex)
        { return new Column(columnName, columnField, searchable, sortable, new Search(searchValue, searchRegex)); }
        public static IOptions MockOptions()
        { return new Options(); }
        public static ModelBinder MockModelBinder()
        { return new ModelBinder(); }
        public static IDataTablesRequest MockDataTablesRequest(int draw, int start, int length, ISearch search, IEnumerable<IColumn> columns)
        { return MockDataTablesRequest(draw, start, length, search, columns, null); }
        public static IDataTablesRequest MockDataTablesRequest(int draw, int start, int length, ISearch search, IEnumerable<IColumn> columns, IDictionary<string, object> additionalParameters)
        { return new DataTablesRequest(draw, start, length, search, columns, additionalParameters); }
        public static IEnumerable<MockData> MockData()
        { return new [] { new MockData("FirstElement"), new MockData("SecondElement"), new MockData("ThirdElement") }; }
        public static IDictionary<string, object> MockAdditionalParameters()
        { return new Dictionary<string, object>() { { "firstParameter", "firstValue" }, { "secondParameter", 7 } }; }
        public static Core.ISearch MockSearch(string searchValue, bool isRegex)
        { return new Search(searchValue, isRegex); }
        public static Core.ISort MockSort(int order, string direction)
        { return new Sort(order, direction); }
        public static IEnumerable<IColumn> MockColumns()
        { 
            return new Column[]
            {
                new Column("column0_name", "column0_field", true, true, MockSearch("column0_search_value", true)),
                new Column("column1_name", "column1_field", true, false, MockSearch("column1_search_value", false)),
                new Column("column2_name", "column2_field", false, true, MockSearch("column2_search_value", true)),
            };
        }
        public static ModelBindingContext MockModelBindingContextWithCamelCase(string draw, string length, string start, string searchValue, string searchRegex)
        { return MockModelBindingContextWithCamelCase(draw, length, start, searchValue, searchRegex, null); }
        public static ModelBindingContext MockModelBindingContextWithCamelCase(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> additionalParameters)
        { return MockModelBindingContext(draw, length, start, searchValue, searchRegex, additionalParameters, new NameConvention.CamelCaseRequestNameConvention()); }
        public static ModelBindingContext MockModelBindingContextWithHungarianNotation(string draw, string length, string start, string searchValue, string searchRegex)
        { return MockModelBindingContextWithHungarianNotation(draw, length, start, searchValue, searchRegex, null); }
        public static ModelBindingContext MockModelBindingContextWithHungarianNotation(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> additionalParameters)
        { return MockModelBindingContext(draw, length, start, searchValue, searchRegex, additionalParameters, new NameConvention.HungarianNotationRequestNameConvention()); }
        public static ModelBindingContext MockModelBindingContext(string draw, string length, string start, string searchValue, string searchRegex, IDictionary<string, object> additionalParameters, Core.NameConvention.IRequestNameConvention requestNameConvention)
        {
            // Request properties.
            var formCollection = new Dictionary<string, StringValues>()
            {
                { requestNameConvention.Length, length },
                { requestNameConvention.Start, start },
                { requestNameConvention.SearchValue, searchValue },
                { requestNameConvention.IsSearchRegex, searchRegex }
            };
            if (!String.IsNullOrWhiteSpace(draw)) formCollection.Add(requestNameConvention.Draw, draw);

            // Aditional parameters.
            if (additionalParameters != null)
            {
                foreach (var keypair in additionalParameters)
                {
                    formCollection.Add(keypair.Key, Convert.ToString(keypair.Value));
                }
            }

            // Value provider for request properties.
            var valueProvider = new FormValueProvider(new BindingSource("a", "a", false, true), new FormCollection(formCollection), new System.Globalization.CultureInfo("en-US"));


            var x = new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(null));
            var metadata = x.GetMetadataForType(typeof(IDataTablesRequest));

            //var mockModelBinderProviderContext = new Mock<ModelBinderProviderContext>();
            //mockModelBinderProviderContext
            //    .Setup(x => x.Metadata)
            //    .Returns(metadata);

            // Model metadata.
            //new Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.ICompositeMetadataDetailsProvider


            //new Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider()
            //new Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider()
            //var x = new DefaultCompositeMetadataDetailsProvider(null);
            //var modelMetadata = new Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider(x).GetMetadataForType(typeof(Core.IDataTablesRequest));
            //var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata. ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Core.IDataTablesRequest));

            return new DefaultModelBindingContext()
            {
                ModelName = "moq",
                ValueProvider = valueProvider,
                ModelMetadata = metadata
            };
        }        
        public static ModelBindingContext MockModelBindingContextWithInvalidRequest()
        {
            // Request properties.
            var formCollection = new Dictionary<string, StringValues>();

            // Value provider for request properties.
            var valueProvider = new FormValueProvider(new BindingSource("a", "a", false, true), new FormCollection(formCollection), new System.Globalization.CultureInfo("en-US"));

            // Model metadata.
            //var x = new DefaultCompositeMetadataDetailsProvider(null);
            //var modelMetadata = new Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DefaultModelMetadataProvider(x).GetMetadataForType(typeof(Core.IDataTablesRequest));
            //var modelMetadata = new Microsoft.AspNet.Mvc.ModelBinding.Metadata. ModelMetadataProviders.Current.GetMetadataForType(null, typeof(Core.IDataTablesRequest));

            var x = new DefaultModelMetadataProvider(new DefaultCompositeMetadataDetailsProvider(null));
            var metadata = x.GetMetadataForType(typeof(IDataTablesRequest));

            return new DefaultModelBindingContext()
            {
                ModelName = "moq",
                ValueProvider = valueProvider,
                ModelMetadata = metadata
            };
        }
        public static IDictionary<string, object> ParseAdditionalParameters(ModelBindingContext modelBindingContext)
        {
            var _return = new Dictionary<string, object>();

            var firstParameter = modelBindingContext.ValueProvider.GetValue("firstParameter");
            _return.Add("firstParameter", firstParameter.FirstValue);

            var secondParameter = modelBindingContext.ValueProvider.GetValue("secondParameter");
            _return.Add("secondParameter", Convert.ToInt32(secondParameter.FirstValue));

            return _return;
        }
    }
}
