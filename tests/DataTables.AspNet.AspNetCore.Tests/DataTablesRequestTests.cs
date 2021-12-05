using System.Linq;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
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
            Assert.True(request.Search.IsRegex);

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
            var aditionalParameters = TestHelper.MockAdditionalParameters(); 
            var request = TestHelper.MockDataTablesRequest(3, 13, 99, search, columns, aditionalParameters);

            // Assert
            Assert.Equal(2, request.AdditionalParameters.Count);
            Assert.Equal("firstValue", request.AdditionalParameters["firstParameter"]);
            Assert.Equal(7, request.AdditionalParameters["secondParameter"]);
        }
    }
}
