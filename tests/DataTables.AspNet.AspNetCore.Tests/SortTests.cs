using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    public class SortTests
    {
        /// <summary>
        /// Validates ascending sort creation.
        /// </summary>
        [Fact]
        public void AscendingSortCreation()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var sort = TestHelper.MockSort(9, options.RequestNameConvention.SortAscending);

            // Assert
            Assert.Equal(9, sort.Order);
            Assert.Equal(Core.SortDirection.Ascending, sort.Direction);
        }
        /// <summary>
        /// Validates descending sort creation.
        /// </summary>
        [Fact]
        public void DescendingSortCreation()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var sort = TestHelper.MockSort(9, options.RequestNameConvention.SortDescending);

            // Assert
            Assert.Equal(9, sort.Order);
            Assert.Equal(Core.SortDirection.Descending, sort.Direction);
        }
    }
}
