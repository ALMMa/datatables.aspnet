using System;
using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 'Column' class.
    /// </summary>
    public class ColumnTests
    {
        /// <summary>
        /// Validates non-searchable column creation.
        /// </summary>
        [Fact]
        public void NonSearchableColumn()
        {
            // Arrange
            var column = TestHelper.MockColumn("mockName", "mockField", false, true);

            // Assert
            Assert.Equal("mockName", column.Name);
            Assert.Equal("mockField", column.Field);
            Assert.Equal(false, column.IsSearchable);
            Assert.Equal(true, column.IsSortable);
            Assert.Equal(null, column.Search);
        }
        /// <summary>
        /// Validates searchable column creation with sarch values.
        /// </summary>
        [Fact]
        public void SearchableColumnWithSearchValue()
        {
            // Arrange
            var column = TestHelper.MockColumn("mockName", "mockField", true, true, "searchValue", true);

            // Assert
            Assert.Equal("mockName", column.Name);
            Assert.Equal("mockField", column.Field);
            Assert.Equal(true, column.IsSearchable);
            Assert.Equal(true, column.IsSortable);
            Assert.Equal("searchValue", column.Search.Value);
            Assert.Equal(true, column.Search.IsRegex);
        }
        /// <summary>
        /// Validate searchable column creation without search values.
        /// </summary>
        [Fact]
        public void SearchableColumnWithoutSearchValue()
        {
            // Arrange
            var column = TestHelper.MockColumn("mockName", "mockField", true, true);

            // Assert
            Assert.Equal("mockName", column.Name);
            Assert.Equal("mockField", column.Field);
            Assert.Equal(true, column.IsSearchable);
            Assert.Equal(true, column.IsSortable);
            Assert.Equal(String.Empty, column.Search.Value);
            Assert.Equal(false, column.Search.IsRegex);
        }
        /// <summary>
        /// Validates setting sort into sortable column.
        /// </summary>
        [Fact]
        public void SetColumnSortOnSortableColumn()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "columnField", false, true);

            // Act
            var orderSet = column.SetSort(3, "desc");

            // Assert
            Assert.Equal(true, orderSet);
            Assert.Equal(3, column.Sort.Order);
            Assert.Equal(Core.SortDirection.Descending, column.Sort.Direction);
        }
        /// <summary>
        /// Validates setting sort into non-sortable column.
        /// </summary>
        [Fact]
        public void SetColumnSortOnNonSortableColumn()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "columnField", false, false);

            // Act
            var orderSet = column.SetSort(3, "desc");

            // Assert
            Assert.Equal(false, orderSet);
            Assert.Null(column.Sort);
        }
    }
}
