using Xunit;
using DataTables.AspNet.Core;
using DapperExtensions.Predicate;
using System;

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
            Assert.Equal(Operator.Like, ((IFieldPredicate)predicate).Operator);
            Assert.Equal("searchValue", ((IFieldPredicate)predicate).Value);
            Assert.Equal("Name", ((IFieldPredicate)predicate).PropertyName);
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
            var predicate = column.GetFilterPredicate<SampleEntity>(column.Search?.Value, true);

            // Assert
            Assert.Equal(Operator.Eq, ((IFieldPredicate)predicate).Operator);
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
            Assert.Equal(Operator.Eq, ((IFieldPredicate)predicate).Operator);
            Assert.Equal("1", ((IFieldPredicate)predicate).Value);
            Assert.Equal("Id", ((IFieldPredicate)predicate).PropertyName);
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
            Assert.True(predicate.Ascending);
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
            Assert.False(predicate.Ascending);
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
            var columnCollection = Array.Empty<IColumn>();

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
