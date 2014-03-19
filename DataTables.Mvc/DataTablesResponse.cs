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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTables.Mvc
{
    /// <summary>
    /// Represents a server-side response for use with DataTables.
    /// </summary>
    /// <remarks>
    /// Variable syntax matches DataTables names to avoid error and avoid aditional parse.
    /// </remarks>
    public class DataTablesResponse
    {
        /// <summary>
        /// Gets the draw counter for DataTables.
        /// </summary>
        public int draw { get; private set; }
        /// <summary>
        /// Gets the data collection.
        /// </summary>
        public IEnumerable data { get; private set; }
        /// <summary>
        /// Gets the total number of records (without filtering - total dataset).
        /// </summary>
        public int recordsTotal { get; private set; }
        /// <summary>
        /// Gets the resulting number of records after filtering.
        /// </summary>
        public int recordsFiltered { get; private set; }
        /// <summary>
        /// Creates a new DataTables response object with it's elements.
        /// </summary>
        /// <param name="draw">The draw counter as received from the DataTablesRequest.</param>
        /// <param name="data">The data collection (data page).</param>
        /// <param name="recordsFiltered">The resulting number of records after filtering.</param>
        /// <param name="recordsTotal">The total number of records (total dataset).</param>
        public DataTablesResponse(int draw, IEnumerable data, int recordsFiltered, int recordsTotal)
        {
            this.draw = draw;
            this.data = data;
            this.recordsFiltered = recordsFiltered;
            this.recordsTotal = recordsTotal;
        }
    }
}
