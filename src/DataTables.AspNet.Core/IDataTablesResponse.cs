using System.Collections.Generic;

namespace DataTables.AspNet.Core
{
    /// <summary>
    /// Defines a DataTables response.
    /// </summary>
    public interface IDataTablesResponse
    {
        /// <summary>
        /// Gets draw counter.
        /// </summary>
        int Draw { get; }
        /// <summary>
        /// Gets total records counter.
        /// </summary>
        int TotalRecords { get; }
        /// <summary>
        /// Gets total filtered records counter.
        /// </summary>
        int TotalRecordsFiltered { get; }
        /// <summary>
        /// Gets data for the response.
        /// </summary>
        object Data { get; }
        /// <summary>
        /// Gets the error message.
        /// </summary>
        string Error { get; }
        /// <summary>
        /// Gets aditional parameters to send back to the client-side.
        /// </summary>
        IDictionary<string, object> AdditionalParameters { get; }
    }
}
