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
    /// Stores parameters and configs from DataTables search engine.
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Gets the value of the search.
        /// </summary>
        public string Value { get; private set; }
        /// <summary>
        /// Indicates if the value of the search is a regex value or not.
        /// </summary>
        public bool IsRegexValue { get; private set; }
        /// <summary>
        /// Creates a new search values holder object.
        /// </summary>
        /// <param name="value">The value of the search.</param>
        /// <param name="isRegexValue">True if the value is a regex value or false otherwise.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the provided search value is null.</exception>
        public Search(string value, bool isRegexValue)
        {
            if (value == null) throw new ArgumentNullException("value", "The value of the search cannot be null. If there's no search performed, provide an empty string.");
            
            this.Value = value;
            this.IsRegexValue = isRegexValue;
        }
    }
}
