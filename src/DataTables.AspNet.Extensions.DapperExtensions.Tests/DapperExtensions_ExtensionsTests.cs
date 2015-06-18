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
            var search = TestHelper.MockSearch("searchValue", false, "Name");

            // Act
            var predicate = search.GetFilterPredicate<SampleEntity>();

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
            var search = TestHelper.MockSearch("searchValue", false, "OtherName");

            // Act
            var predicate = search.GetFilterPredicate<SampleEntity>();

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
            var search = TestHelper.MockSearch("searchValue", false, "Name");

            // Act
            var predicate = search.GetFilterPredicate<SampleEntity>(true);

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
            var search = TestHelper.MockSearch("1", false, "Id");

            // Act
            var predicate = search.GetFilterPredicate<SampleEntity>();

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
            var sort = TestHelper.MockSort("Name", 1, options.RequestNameConvention.SortAscending);

            // Act
            var predicate = sort.GetSortPredicate<SampleEntity>();

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
            var sort = TestHelper.MockSort("OtherName", 1, options.RequestNameConvention.SortAscending);

            // Act
            var predicate = sort.GetSortPredicate<SampleEntity>();

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
            var sort = TestHelper.MockSort("Name", 1, options.RequestNameConvention.SortDescending);

            // Act
            var predicate = sort.GetSortPredicate<SampleEntity>();

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
            var sort1 = TestHelper.MockSort("Id", 0, options.RequestNameConvention.SortAscending);
            var sort2 = TestHelper.MockSort("Name", 1, options.RequestNameConvention.SortDescending);
            var sortCollection = new ISort[] { sort1, sort2 };

            // Act
            var collection = sortCollection.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Equal(2, collection.Count);
        }
        /// <summary>
        /// Validates sort translation of an empty collection.
        /// </summary>
        [Fact]
        public void SortEmptyCollection()
        {
            // Arrange
            var sortCollection = new ISort[0];

            // Act
            var collection = sortCollection.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Equal(0, collection.Count);
        }
        /// <summary>
        /// Validates sort translation of a null object.
        /// </summary>
        [Fact]
        public void SortNullCollection()
        {
            // Arrange
            ISort[] sortCollection = null;

            // Act
            var collection = sortCollection.GetSortPredicate<SampleEntity>();

            // Assert
            Assert.Null(collection);
        }
    }
}
