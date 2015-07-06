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

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataTables.AspNet.AspNet5.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'DataTablesRequest' class.
    /// </summary>
    public class DataTablesRequestTests
    {
        /// <summary>
        /// Validates standard (complete) request creation.
        /// </summary>
        [Fact]
        public void RequestCreation()
        {
            // Arrange
            var search = TestHelper.MockSearch("searchValue", true);
            var columns = TestHelper.MockColumns();
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, search, columns);

            // Assert
            Assert.Equal(3, request.Draw);
            Assert.Equal(13, request.Start);
            Assert.Equal(99, request.Length);

            Assert.Equal("searchValue", request.Search.Value);
            Assert.Equal(true, request.Search.IsRegex);

            Assert.Equal(3, request.Columns.Count());
            foreach (var column in request.Columns) Assert.NotNull(column);
        }
        /// <summary>
        /// Validares request creation with aditional parameters.
        /// </summary>
        [Fact]
        public void RequestWithAditionalParameters()
        {
            // Arrange
            var search = TestHelper.MockSearch("searchValue", true);
            var columns = TestHelper.MockColumns();
            var aditionalParameters = TestHelper.MockAditionalParameters(); 
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, search, columns, aditionalParameters);

            // Assert
            Assert.Equal(2, request.AditionalParameters.Count);
            Assert.Equal("firstValue", request.AditionalParameters["firstParameter"]);
            Assert.Equal(7, request.AditionalParameters["secondParameter"]);
        }
    }
}
