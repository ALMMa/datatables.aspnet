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

namespace DataTables.AspNet.WebApi2.Tests
{
    /// <summary>
    /// Represents tests for DataTables.AspNet.Mvc5 naming conventions.
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
            Assert.Equal("asc", names.SortAscending);
            Assert.Equal("desc", names.SortDescending);
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
            Assert.Equal("asc", names.SortAscending);
            Assert.Equal("desc", names.SortDescending);
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
            Assert.Equal(String.Empty, names.Error);
            Assert.Equal("aaData", names.Data);
            Assert.Equal("iTotalRecords", names.TotalRecords);
            Assert.Equal("iTotalDisplayRecords", names.TotalRecordsFiltered);
        }
    }
}
