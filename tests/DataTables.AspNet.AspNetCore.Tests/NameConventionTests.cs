using Xunit;

namespace DataTables.AspNet.AspNetCore.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.AspNet5 naming conventions.
    /// </summary>
    public class NameConventionTests
    {
        /// <summary>
        /// Validates CamelCase request name convention.
        /// </summary>
        [Fact]
        public void CamelCaseRequestNameConvention()
        {
            // Arrange
            var names = new NameConvention.CamelCaseRequestNameConvention();

            // Assert
            Assert.Equal("draw", names.Draw);
            Assert.Equal("start", names.Start);
            Assert.Equal("length", names.Length);
            Assert.Equal("search[value]", names.SearchValue);
            Assert.Equal("search[regex]", names.IsSearchRegex);
            Assert.Equal("order[{0}][column]", names.SortField);
            Assert.Equal("order[{0}][dir]", names.SortDirection);
            Assert.Equal("columns[{0}][data]", names.ColumnField);
            Assert.Equal("columns[{0}][name]", names.ColumnName);
            Assert.Equal("columns[{0}][searchable]", names.IsColumnSearchable);
            Assert.Equal("columns[{0}][orderable]", names.IsColumnSortable);
            Assert.Equal("columns[{0}][search][value]", names.ColumnSearchValue);
            Assert.Equal("columns[{0}][search][regex]", names.IsColumnSearchRegex);
        }
        /// <summary>
        /// Validates CamelCase response name convention.
        /// </summary>
        [Fact]
        public void CamelCaseResponseNameConvention()
        {
            // Arrange
            var names = new NameConvention.CamelCaseResponseNameConvention();

            // Assert
            Assert.Equal("draw", names.Draw);
            Assert.Equal("error", names.Error);
            Assert.Equal("data", names.Data);
            Assert.Equal("recordsTotal", names.TotalRecords);
            Assert.Equal("recordsFiltered", names.TotalRecordsFiltered);
        }



        /// <summary>
        /// Validates HungarianNotation request name convention.
        /// </summary>
        [Fact]
        public void HungarianNotationRequestNameConvention()
        {
            // Arrange
            var names = new NameConvention.HungarianNotationRequestNameConvention();

            // Assert
            Assert.Equal("sEcho", names.Draw);
            Assert.Equal("iDisplayStart", names.Start);
            Assert.Equal("iDisplayLength", names.Length);
            Assert.Equal("sSearch", names.SearchValue);
            Assert.Equal("bRegex", names.IsSearchRegex);
            Assert.Equal("sSortCol_{0}", names.SortField);
            Assert.Equal("sSortDir_{0}", names.SortDirection);
            Assert.Equal("mDataProp_{0}", names.ColumnField);
            Assert.Equal("{0}", names.ColumnName);
            Assert.Equal("bSearchable_{0}", names.IsColumnSearchable);
            Assert.Equal("bSortable_{0}", names.IsColumnSortable);
            Assert.Equal("sSearch_{0}", names.ColumnSearchValue);
            Assert.Equal("bRegex_{0}", names.IsColumnSearchRegex);
        }
        /// <summary>
        /// Validates HungarianNotation response name convention.
        /// </summary>
        [Fact]
        public void HungarianNotationResponseNameConvention()
        {
            // Arrange
            var names = new NameConvention.HungarianNotationResponseNameConvention();

            // Assert
            Assert.Equal("sEcho", names.Draw);
            Assert.Equal(string.Empty, names.Error);
            Assert.Equal("aaData", names.Data);
            Assert.Equal("iTotalRecords", names.TotalRecords);
            Assert.Equal("iTotalDisplayRecords", names.TotalRecordsFiltered);
        }
    }
}
