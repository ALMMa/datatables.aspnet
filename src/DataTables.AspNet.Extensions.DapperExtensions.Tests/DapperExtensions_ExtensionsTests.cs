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
using DataTables.AspNet.Core;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet extensions for DapperExtensions.
    /// </summary>
    public class DapperExtensions_ExtensionsTests
    {
        /// <summary>
        /// Validates search translation on a string field.
        /// </summary>
        [Fact]
        public void DefaultSearchOnStringFieldWithExistingFieldName()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "Name", true, true, "searchValue", false);

            // Act
            var predicate = column.GetFilterPredicate<SampleEntity>();

            // Assert
            Assert.Equal(global::DapperExtensions.Operator.Like, ((global::DapperExtensions.IFieldPredicate)predicate).Operator);
            Assert.Equal("searchValue", ((global::DapperExtensions.IFieldPredicate)predicate).Value);
            Assert.Equal("Name", ((global::DapperExtensions.IFieldPredicate)predicate).PropertyName);
        }
        /// <summary>
        /// Validates search translation on a non-existing field.
        /// </summary>
        [Fact]
        public void DefaultSearchOnStringFieldWithNonExistingFieldName()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "OtherName", true, true, "searchValue", false);

            // Act
            var predicate = column.GetFilterPredicate<SampleEntity>();

            // Assert
            Assert.Null(predicate);
        }
        /// <summary>
        /// Validates search translation with equalsTo being forced on a string field.
        /// </summary>
        [Fact]
        public void ForceEqualsToSearchOnStringField()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "Name", true, true, "searchValue", false);

            // Act
            var predicate = column.GetFilterPredicate<SampleEntity>(true);

            // Assert
            Assert.Equal(global::DapperExtensions.Operator.Eq, ((global::DapperExtensions.IFieldPredicate)predicate).Operator);
        }
        /// <summary>
        /// Validates search translation on an integer field.
        /// </summary>
        [Fact]
        public void DefaultSearchOnIntField()
        {
            // Arrange
            var column = TestHelper.MockColumn("columnName", "Id", true, true, "1", false);

            // Act
            var predicate = column.GetFilterPredicate<SampleEntity>();

            // Assert
            Assert.Equal(global::DapperExtensions.Operator.Eq, ((global::DapperExtensions.IFieldPredicate)predicate).Operator);
            Assert.Equal("1", ((global::DapperExtensions.IFieldPredicate)predicate).Value);
            Assert.Equal("Id", ((global::DapperExtensions.IFieldPredicate)predicate).PropertyName);
        }
        /// <summary>
        /// Validates sort translation for a sort on an existing field.
        /// </summary>
        [Fact]
        public void DefaultSortOnExistingField()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var column = TestHelper.MockColumn("columnName", "Name", true, true, null, false);
            column.SetSort(1, options.RequestNameConvention.SortAscending);

            // Act
            var predicate = column.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Equal(true, predicate.Ascending);
            Assert.Equal("Name", predicate.PropertyName);
        }
        /// <summary>
        /// Validates sort translation for a sort with a non-existing field.
        /// </summary>
        [Fact]
        public void DefaultSortOnNonExistingField()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var column = TestHelper.MockColumn("columnName", "OtherName", true, true, null, false);
            column.SetSort(1, options.RequestNameConvention.SortAscending);

            // Act
            var predicate = column.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Null(predicate);
        }
        /// <summary>
        /// Validates descending sort translation.
        /// </summary>
        [Fact]
        public void SortDescending()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var column = TestHelper.MockColumn("columnName", "Name", true, true, null, false);
            column.SetSort(1, options.RequestNameConvention.SortDescending);

            // Act
            var predicate = column.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Equal(false, predicate.Ascending);
        }
        /// <summary>
        /// Validates sort translation of a valid collection.
        /// </summary>
        [Fact]
        public void SortValidCollection()
        {
            // Arrange
            var options = TestHelper.MockOptions();
            var column1 = TestHelper.MockColumn("columnName1", "Id", true, true, null, false);
            column1.SetSort(1, options.RequestNameConvention.SortAscending);
            var column2 = TestHelper.MockColumn("columnName2", "Name", true, true, null, false);
            column2.SetSort(1, options.RequestNameConvention.SortDescending);
            var columns = new IColumn[] { column1, column2 };

            // Act
            var columnCollection = columns.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Equal(2, columnCollection.Count);
        }
        /// <summary>
        /// Validates sort translation of an empty collection.
        /// </summary>
        [Fact]
        public void SortEmptyCollection()
        {
            // Arrange
            var columnCollection = new IColumn[0];

            // Act
            var collection = columnCollection.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Null(collection);
        }
        /// <summary>
        /// Validates sort translation of a null object.
        /// </summary>
        [Fact]
        public void SortNullCollection()
        {
            // Arrange
            IColumn[] columnCollection = null;

            // Act
            var collection = columnCollection.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Null(collection);
        }
    }
}
