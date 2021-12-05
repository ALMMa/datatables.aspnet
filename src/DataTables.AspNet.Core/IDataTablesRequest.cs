using System.Collections.Generic;

namespace DataTables.AspNet.Core
{
    /// <summary>
    /// Defined a DataTables request.
    /// </summary>
    public interface IDataTablesRequest
    {
        /// <summary>
        /// Gets draw counter.
        /// This is used by DataTables to ensure that the Ajax returns from server-side procesing request are drawn in sequence by DataTables.
        /// Ajax requests are asynchronous and thus can return out of sequence.
        /// This is used as part of the 'Draw' return parameter.
        /// </summary>
        int Draw { get; }

        /// <summary>
        /// Gets paging first record indicator.
        /// This is the start point in the current data set (zero index based).
        /// </summary>
        int Start { get; }

        /// <summary>
        /// Gets the number of records that the table can display in the current draw.
        /// It is expected that the number of records returned will be equal to this number, unless the server has fewer records to return.
        /// Note that this can be -1 to indicate that all records should be returned (although that negates any benefits of server-side processing!).
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets global search.
        /// To be applied to all searchable columns.
        /// </summary>
        ISearch Search { get; }

        /// <summary>
        /// Gets DataTables column collection (from client-side).
        /// </summary>
        IEnumerable<IColumn> Columns { get; }


        /// <summary>
        /// Gets the user-defined collection of parameters.
        /// </summary>
        IDictionary<string, object> AdditionalParameters { get; }
    }
}
