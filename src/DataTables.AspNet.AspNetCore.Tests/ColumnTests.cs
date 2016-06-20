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
