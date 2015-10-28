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
using DataTables.AspNet.Core;

namespace DataTables.AspNet.WebApi2
{
    /// <summary>
    /// Represents search/filter definition and value.
    /// </summary>
    public class Search : ISearch
    {
        /// <summary>
        /// Gets the search data field if bound to a column.
        /// Field for global search is null.
        /// </summary>
        public string Field { get; private set; }
        /// <summary>
        /// Gets an indicator if search value is regex or plain text.
        /// </summary>
        public bool IsRegex { get; private set; }
        /// <summary>
        /// Gets search value.
        /// </summary>
        public string Value { get; private set; }



        /// <summary>
        /// Creates a new search instance.
        /// </summary>
        public Search()
            : this(String.Empty, false, null)
        { }
        /// <summary>
        /// Creates a new search instance.
        /// </summary>
        /// <param name="field">Data field name to be bound.</param>
        public Search(string field)
            : this(String.Empty, false, field)
        { }
        /// <summary>
        /// Creates a new search instance.
        /// </summary>
        /// <param name="value">Search value.</param>
        /// <param name="isRegex">True if search value is regex, False if search value is plain text.</param>
        public Search(string value, bool isRegex)
            : this (value, isRegex, null)
        { }
        /// <summary>
        /// Creates a new search instance.
        /// </summary>
        /// <param name="value">Search value.</param>
        /// <param name="isRegex">True if search value is regex, False if search value is plain text.</param>
        /// <param name="field">Data field name to be bound.</param>
        public Search(string value, bool isRegex, string field)
        {
            Field = field;
            Value = value;
            IsRegex = isRegex;
        }
    }
}
