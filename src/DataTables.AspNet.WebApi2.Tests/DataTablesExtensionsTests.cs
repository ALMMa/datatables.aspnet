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

using Xunit;

namespace DataTables.AspNet.WebApi2.Tests
{
    public class DataTablesExtensionsTests
    {
        /// <summary>
        /// Validates error response creation without aditional parameters.
        /// </summary>
        [Fact]
        public void ErrorResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);

            // Act
            var response = request.CreateResponse("just_some_error_message");

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates error response creation with aditional parameters.
        /// </summary>
        [Fact]
        public void ErrorResponseWithAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var aditionalParameters = TestHelper.MockAditionalParameters();

            // Act
            var response = request.CreateResponse("just_some_error_message", aditionalParameters);

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates response creation without aditional parameters.
        /// </summary>
        [Fact]
        public void ResponseWithoutAditionalParameters()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, null, null);
            var data = TestHelper.MockData();

            // Act
            var response = request.CreateResponse(2000, 1000, data);

            // Assert
            Assert.NotNull(response);
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
            var aditionalParameters = TestHelper.MockAditionalParameters();

            // Act
            var response = request.CreateResponse(2000, 1000, data, aditionalParameters);

            // Assert
            Assert.NotNull(response);
        }
        /// <summary>
        /// Validates response response creation for invalid (null) request.
        /// </summary>
        [Fact]
        public void NullRequestResponseCreation()
        {
            // Arrange
            Core.IDataTablesRequest request = null;

            // Act
            var response = request.CreateResponse("just_some_error_message");

            // Assert
            Assert.Null(response);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validates response creation for request with invalid draw value and draw validtion enabled.
        /// </summary>
        [Fact]
        public void InvalidDrawResponseCreationWithDrawValidation()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(0, 13, 99, null, null);
            var data = TestHelper.MockData();
            DataTables.AspNet.WebApi2.Configuration.Options.EnableDrawValidation();

            // Act
            var response = request.CreateResponse(2000, 1000, data);

            // Assert
            Assert.Null(response);
        }
        /// <summary>
        /// This test must be executed alone.
        /// Validates response creation for request with invalid draw value and without draw validtion enabled.
        /// </summary>
        [Fact]
        public void InvalidDrawResponseCreationWithoutDrawValidation()
        {
            // Arrange
            var request = TestHelper.MockDataTablesRequest(0, 13, 99, null, null);
            var data = TestHelper.MockData();
            DataTables.AspNet.WebApi2.Configuration.Options.DisableDrawValidation();

            // Act
            var response = request.CreateResponse(2000, 1000, data);

            // Assert
            Assert.NotNull(response);
        }
    }
}
