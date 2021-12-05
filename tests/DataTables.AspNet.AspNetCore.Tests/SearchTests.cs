using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'Searc' class.
    /// </summary>
    public class SearchTests
    {
        /// <summary>
        /// Validates search creation without data field.
        /// </summary>
        [Fact]
        public void SearchCreationWithoutField()
        {
            var search = TestHelper.MockSearch("searchValue", true);

            Assert.Equal("searchValue", search.Value);
            Assert.Equal(true, search.IsRegex);
        }
        /// <summary>
        /// Validates search creation with data field.
        /// </summary>
        [Fact]
        public void SearchCreationWithField()
        {
            var search = TestHelper.MockSearch("searchValue", true);

            Assert.Equal("searchValue", search.Value);
            Assert.Equal(true, search.IsRegex);
        }
    }
}
