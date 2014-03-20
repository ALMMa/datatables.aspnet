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

namespace DataTables.Mvc
{
    /// <summary>
    /// Defines a server-side request for use with DataTables.
    /// </summary>
    /// <remarks>
    /// Variable syntax does NOT match DataTables names because auto-mapping won't work anyway.
    /// Use the DataTablesModelBinder or provide your own binder to bind your model with DataTables's request.
    /// </remarks>
    public interface IDataTablesRequest
    {
        /// <summary>
        /// Gets the draw counter from client-side to give back on the server's response.
        /// </summary>
        int Draw { get; }
        /// <summary>
        /// Gets the start record number (count) for paging.
        /// </summary>
        int Start { get; }
        /// <summary>
        /// Gets the length of the page (max records per page).
        /// </summary>
        int Length { get; }
        /// <summary>
        /// Gets the global search pagameters.
        /// </summary>
        Search Search { get; }
        /// <summary>
        /// Gets the read-only collection of client-side columns with their options and configs.
        /// </summary>
        ColumnCollection Columns { get; }
    }
    /// <summary>
    /// For internal use only.
    /// Represents DataTables request parameters.
    /// </summary>
    class DataTablesRequest : IDataTablesRequest
    {
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the draw counter from DataTables.
        /// </summary>
        public int Draw { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the start record number (jump) for paging.
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the length of the page (paging).
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the global search term.
        /// </summary>
        public Search Search { get; set; }
        /// <summary>
        /// For internal use only.
        /// Gets/Sets the column collection.
        /// </summary>
        public ColumnCollection Columns { get; private set; }
        /// <summary>
        /// For internal use only.
        /// Set the new columns on the mechanism.
        /// </summary>
        /// <param name="columns">The columns to be set.</param>
        public void SetColumns(IEnumerable<Column> columns) { Columns = new ColumnCollection(columns); }
    }
}