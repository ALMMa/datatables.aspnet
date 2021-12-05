using System;
using DataTables.AspNet.Core;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents search/filter definition and value.
    /// </summary>
    public class Search : ISearch
    {
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
            : this(String.Empty, false)
        { }
        /// <summary>
        /// Creates a new search instance.
        /// </summary>
        /// <param name="value">Search value.</param>
        /// <param name="isRegex">True if search value is regex, False if search value is plain text.</param>
        public Search(string value, bool isRegex)
        {
            Value = value;
            IsRegex = isRegex;
        }
    }
}
