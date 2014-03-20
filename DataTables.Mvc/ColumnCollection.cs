#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTables.Mvc
{
    /// <summary>
    /// Represents a read-only DataTables column collection.
    /// </summary>
    public class ColumnCollection : IEnumerable<Column>
    {
        /// <summary>
        /// For internal use only.
        /// Stores data.
        /// </summary>
        private IReadOnlyList<Column> Data;
        /// <summary>
        /// Created a new ReadOnlyColumnCollection with predefined data.
        /// </summary>
        /// <param name="columns">The column collection from DataTables.</param>
        public ColumnCollection(IEnumerable<Column> columns)
        {
            if (columns == null) throw new ArgumentNullException("The provided column collection cannot be null", "columns");
            Data = columns.ToList().AsReadOnly();
        }
        /// <summary>
        /// Get sorted columns on client-side already on the same order as the client requested.
        /// The method checks if the column is bound and if it's ordered on client-side.
        /// </summary>
        /// <returns>The ordered enumeration of sorted columns.</returns>
        public IOrderedEnumerable<Column> GetSortedColumns()
        {
            return Data
                .Where(_column => !String.IsNullOrWhiteSpace(_column.Data) && _column.IsOrdered)
                .OrderBy(_c => _c.OrderNumber);
        }
        /// <summary>
        /// Get filtered columns on client-side.
        /// The method checks if the column is bound and if the search has a value.
        /// </summary>
        /// <returns>The enumeration of filtered columns.</returns>
        public IEnumerable<Column> GetFilteredColumns()
        {
            return Data
                .Where(_column => !String.IsNullOrWhiteSpace(_column.Data) && _column.Searchable && !String.IsNullOrWhiteSpace(_column.Search.Value));
        }
        /// <summary>
        /// Returns the enumerable element as defined on IEnumerable.
        /// </summary>
        /// <returns>The enumerable elemento to iterate through data.</returns>
        public IEnumerator<Column> GetEnumerator()
        {
            return Data.GetEnumerator();
        }
        /// <summary>
        /// Returns the enumerable element as defined on IEnumerable.
        /// </summary>
        /// <returns>The enumerable element to iterate through data.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)Data).GetEnumerator();
        }
    }
}
