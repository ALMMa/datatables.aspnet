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

namespace DataTables.AspNet.Core.NameConvention
{
    /// <summary>
    /// Define name convention for request parameters.
    /// </summary>
    public interface IRequestNameConvention
    {
        /// <summary>
        /// Gets template for draw.
        /// </summary>
        string Draw { get; }
        /// <summary>
        /// Gets template for data start counter.
        /// </summary>
        string Start { get; }
        /// <summary>
        /// Gets template for data page length.
        /// </summary>
        string Length { get; }
        /// <summary>
        /// Gets template for global search value.
        /// </summary>
        string SearchValue { get; }
        /// <summary>
        /// Gets template for global search regex indicator.
        /// </summary>
        string IsSearchRegex { get; }
        /// <summary>
        /// Gets template for each sort field name.
        /// </summary>
        string SortField { get; }
        /// <summary>
        /// Gets template for each sort direction.
        /// </summary>
        string SortDirection { get; }
        /// <summary>
        /// Gets template for each column field.
        /// </summary>
        string ColumnField { get; }
        /// <summary>
        /// Gets template for each column name.
        /// </summary>
        string ColumnName { get; }
        /// <summary>
        /// Gets template for each column searchable indicator.
        /// </summary>
        string IsColumnSearchable { get; }
        /// <summary>
        /// Gets template for each column sortable indicator.
        /// </summary>
        string IsColumnSortable { get; }
        /// <summary>
        /// Gets template for each column search value.
        /// </summary>
        string ColumnSearchValue { get; }
        /// <summary>
        /// Gets template for each column search regex indicator.
        /// </summary>
        string IsColumnSearchRegex { get; }
        /// <summary>
        /// Gets template for ascending sort.
        /// </summary>
        string SortAscending { get; }
        /// <summary>
        /// Gets template for descending sort.
        /// </summary>
        string SortDescending { get; }
    }
}
