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
        /// This is used by DataTables to ensure that the Ajax returns from server-side processing requests are drawn in sequence by DataTables (Ajax requests are asynchronous and thus can return out of sequence).
        /// This is used as part of the draw return parameter (see below).
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        int Draw { get; }

        /// <summary>
        /// Gets paging first record indicator.
        /// Paging first record indicator. This is the start point in the current data set (0 index based - i.e. 0 is the first record).
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        int Start { get; }

        /// <summary>
        /// Gets the number of records that the table can display in the current draw.
        /// Number of records that the table can display in the current draw.
        /// It is expected that the number of records returned will be equal to this number, unless the server has fewer records to return.
        /// Note that this can be -1 to indicate that all records should be returned (although that negates any benefits of server-side processing!).
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets global search definition.
        /// To be applied to all columns which have searchable as true.
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        ISearch Search { get; }

        /// <summary>
        /// Gets DataTables column collection (from client-side).
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        IEnumerable<IColumn> Columns { get; }


        /// <summary>
        /// Gets the user-defined collection of parameters.
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        IDictionary<string, object> AdditionalParameters { get; }
    }
}
