using DataTables.AspNet.AspNetCore.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'DataTablesResponse' class.
    /// </summary>
    public class DataTablesResponseTests
    {
        /// <summary>
        /// Validates error response creation.
        /// </summary>
        [Fact]
        public async Task ErrorResponse()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            var collection = new ServiceCollection();
            collection.AddMvc();
            collection.AddLogging();

            actionContext.HttpContext.RequestServices = collection.BuildServiceProvider();
            actionContext.HttpContext.Response.Body = new MemoryStream();

            // Act
            var dataTablesResponse = DataTablesResponse<MockData>.Create(request, "just_an_error_message");
            var dataTablesJsonResponse = new DataTablesJsonResponse<MockData>(dataTablesResponse);
            await dataTablesJsonResponse.ExecuteResultAsync(actionContext);

            // Assert
            actionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseContents = await (new StreamReader(actionContext.HttpContext.Response.Body)).ReadToEndAsync();
            var response = JsonConvert.DeserializeAnonymousType(responseContents, new
            {
                Draw = 0,
                Error = "",
                TotalRecords = 0,
                TotalRecordsFiltered = 0,
                Data = new List<MockData>(),
                AdditionalParameters = new Dictionary<string, string>()
            });
            Assert.Equal(request.Draw, response.Draw);
            Assert.Equal("just_an_error_message", response.Error);
            Assert.Equal(0, response.TotalRecords);
            Assert.Equal(0, response.TotalRecordsFiltered);
            Assert.Null(response.Data);
            Assert.Null(response.AdditionalParameters);
        }
        /// <summary>
        /// Validates response creation without aditional parameters dictionary.
        /// </summary>
        [Fact]
        public void ResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data);

            // Assert
            Assert.Equal(request.Draw, response.Draw);
            Assert.Null(response.Error);
            Assert.Equal(2000, response.TotalRecords);
            Assert.Equal(1000, response.TotalRecordsFiltered);
            Assert.Equal(data, response.Data);
            Assert.Null(response.AdditionalParameters);
        }
        /// <summary>
        /// Validates response creation with aditional parameters dictionary.
        /// </summary>
        [Fact]
        public void ResponseWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var aditionalParameters = TestHelper.MockAdditionalParameters();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data, aditionalParameters);

            // Assert
            Assert.Equal(request.Draw, response.Draw);
            Assert.Null(response.Error);
            Assert.Equal(2000, response.TotalRecords);
            Assert.Equal(1000, response.TotalRecordsFiltered);
            Assert.Equal(data, response.Data);
            Assert.Equal(aditionalParameters, response.AdditionalParameters);
        }
        
        /// <summary>
        /// Validates error response serialization.
        /// </summary>
        [Fact]
        public async Task ErrorResponseSerializationWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var expectedJson = string.Format(
                "{{\"{0}\":3,\"{1}\":\"just_an_error_message\",\"{2}\":0,\"{3}\":0,\"{4}\":null,\"{5}\":null}}",
                names.Draw,
                names.Error,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                names.AdditionalParameters);
            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            var collection = new ServiceCollection();
            collection.AddMvc();
            collection.AddLogging();

            actionContext.HttpContext.RequestServices = collection.BuildServiceProvider();
            actionContext.HttpContext.Response.Body = new MemoryStream();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, "just_an_error_message");
            var dataTablesJsonResponse = new DataTablesJsonResponse<MockData>(response);
            await dataTablesJsonResponse.ExecuteResultAsync(actionContext);

            // Assert
            actionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseContents = await (new StreamReader(actionContext.HttpContext.Response.Body)).ReadToEndAsync();
            Assert.Equal(expectedJson, responseContents);
        }

        /// <summary>
        /// Validates error response serialization.
        /// </summary>
        [Fact]
        public async Task ErrorResponseSerializationWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var aditionalParameters = TestHelper.MockAdditionalParameters();
            var expectedJson = string.Format(
                "{{\"{0}\":3,\"{1}\":\"just_an_error_message\",\"{2}\":0,\"{3}\":0,\"{4}\":null,\"{5}\":{{\"firstParameter\":\"firstValue\",\"secondParameter\":7}}}}", 
                names.Draw,
                names.Error,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                names.AdditionalParameters);
            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            var collection = new ServiceCollection();
            collection.AddMvc();
            collection.AddLogging();

            actionContext.HttpContext.RequestServices = collection.BuildServiceProvider();
            actionContext.HttpContext.Response.Body = new MemoryStream();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, "just_an_error_message", aditionalParameters);
            var dataTablesJsonResponse = new DataTablesJsonResponse<MockData>(response);
            await dataTablesJsonResponse.ExecuteResultAsync(actionContext);

            // Assert
            actionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseContents = await(new StreamReader(actionContext.HttpContext.Response.Body)).ReadToEndAsync();
            Assert.Equal(expectedJson, responseContents);
        }

        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        [Fact]
        public async Task ResponseSerializationWithoutAditionalParameters()
        {
            // Arrangevar actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            var collection = new ServiceCollection();
            collection.AddMvc();
            collection.AddLogging();

            actionContext.HttpContext.RequestServices = collection.BuildServiceProvider();
            actionContext.HttpContext.Response.Body = new MemoryStream();

            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var expectedJson = string.Format("{{\"{0}\":3,\"{1}\":null,\"{2}\":2000,\"{3}\":1000,\"{4}\":{5},\"{6}\":null}}",
                names.Draw,
                names.Error,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                JsonConvert.SerializeObject(data, new JsonSerializerSettings {  ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore }),
                names.AdditionalParameters);

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data);


            // Assert
            var dataTablesJsonResponse = new Microsoft.AspNetCore.Mvc.JsonResult(response);
            await dataTablesJsonResponse.ExecuteResultAsync(actionContext);
            actionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseContents = await(new StreamReader(actionContext.HttpContext.Response.Body)).ReadToEndAsync();
            Assert.Equal(expectedJson, responseContents);
        }

        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        [Fact]
        public async Task ResponseSerializationWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var aditionalParameters = TestHelper.MockAdditionalParameters();
            var expectedJson = string.Format("{{\"{0}\":3,\"{1}\":null,\"{2}\":2000,\"{3}\":1000,\"{4}\":{5},\"{6}\":{{\"firstParameter\":\"firstValue\",\"secondParameter\":7}}}}",
                names.Draw,
                names.Error,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                JsonConvert.SerializeObject(data),
                names.AdditionalParameters,
                JsonConvert.SerializeObject(aditionalParameters));
            var actionContext = new Microsoft.AspNetCore.Mvc.ActionContext
            {
                HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
            };

            var collection = new ServiceCollection();
            collection.AddMvc();
            collection.AddLogging();

            actionContext.HttpContext.RequestServices = collection.BuildServiceProvider();
            actionContext.HttpContext.Response.Body = new MemoryStream();

            // Act
            var response = DataTablesResponse<MockData>.Create(request, 2000, 1000, data, aditionalParameters);

            // Assert
            var dataTablesJsonResponse = new Microsoft.AspNetCore.Mvc.JsonResult(response);
            await dataTablesJsonResponse.ExecuteResultAsync(actionContext);
            actionContext.HttpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseContents = await(new StreamReader(actionContext.HttpContext.Response.Body)).ReadToEndAsync();
            Assert.Equal(expectedJson, responseContents);
        }
    }
}
