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
        /// Gets and sets the draw counter from client-side to give back on the server's response.
        /// </summary>
        int Draw { get; set; }
        /// <summary>
        /// Gets and sets the start record number (count) for paging.
        /// </summary>
        int Start { get; set; }
        /// <summary>
        /// Gets and sets the length of the page (max records per page).
        /// </summary>
        int Length { get; set; }
        /// <summary>
        /// Gets and sets the global search pagameters.
        /// </summary>
        Search Search { get; set; }
        /// <summary>
        /// Gets and sets the read-only collection of client-side columns with their options and configs.
        /// </summary>
        ColumnCollection Columns { get; set; }
    }
}