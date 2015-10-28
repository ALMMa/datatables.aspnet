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
using Xunit;

namespace DataTables.AspNet.AspNet5.Tests
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
        public void ErrorResponse()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);

            // Act
            var response = DataTablesResponse.Create(request, "just_an_error_message");

            // Assert
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
            var response = DataTablesResponse.Create(request, 2000, 1000, data);

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
            var response = DataTablesResponse.Create(request, 2000, 1000, data, aditionalParameters);

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
        public void ErrorResponseSerializationWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":\"just_an_error_message\"}}", names.Draw, names.Error);

            // Act
            var response = DataTablesResponse.Create(request, "just_an_error_message");

            // Assert
            Assert.Equal(expectedJson, response.ToString());
        }
        /// <summary>
        /// Validates error response serialization.
        /// </summary>
        [Fact]
        public void ErrorResponseSerializationWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var aditionalParameters = TestHelper.MockAdditionalParameters();
            var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":\"just_an_error_message\",\"firstParameter\":\"firstValue\",\"secondParameter\":7}}", names.Draw, names.Error);

            // Act
            var response = DataTablesResponse.Create(request, "just_an_error_message", aditionalParameters);

            // Assert
            Assert.Equal(expectedJson, response.ToString());
        }
        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        [Fact]
        public void ResponseSerializationWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":2000,\"{2}\":1000,\"{3}\":{4}}}",
                names.Draw,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                Newtonsoft.Json.JsonConvert.SerializeObject(data));

            // Act
            var response = DataTablesResponse.Create(request, 2000, 1000, data);

            // Assert
            Assert.Equal(expectedJson, response.ToString());
        }
        /// <summary>
        /// Validates response serialization without aditional parameters.
        /// </summary>
        [Fact]
        public void ResponseSerializationWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();
            var names = new NameConvention.CamelCaseResponseNameConvention();
            var aditionalParameters = TestHelper.MockAdditionalParameters();
            var expectedJson = String.Format("{{\"{0}\":3,\"{1}\":2000,\"{2}\":1000,\"{3}\":{4},\"firstParameter\":\"firstValue\",\"secondParameter\":7}}",
                names.Draw,
                names.TotalRecords,
                names.TotalRecordsFiltered,
                names.Data,
                Newtonsoft.Json.JsonConvert.SerializeObject(data),
                Newtonsoft.Json.JsonConvert.SerializeObject(aditionalParameters));

            // Act
            var response = DataTablesResponse.Create(request, 2000, 1000, data, aditionalParameters);

            // Assert
            Assert.Equal(expectedJson, response.ToString());
        }
    }
}
