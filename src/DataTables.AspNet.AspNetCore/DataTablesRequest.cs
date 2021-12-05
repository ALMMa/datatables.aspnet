using DataTables.AspNet.Core;
using System.Collections.Generic;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// For internal use only.
    /// Represents a DataTables request.
    /// </summary>
    internal class DataTablesRequest : IDataTablesRequest
    {
        public IDictionary<string, object> AdditionalParameters { get; private set; }
        public IEnumerable<IColumn> Columns { get; private set; }
        public int Draw { get; private set; }
        public int Length { get; private set; }
        public ISearch Search { get; private set; }
        public int Start { get; private set; }

        public DataTablesRequest(int draw, int start, int length, ISearch search, IEnumerable<IColumn> columns)
            :this(draw, start, length, search, columns, null)
        { }

        public DataTablesRequest(int draw, int start, int length, ISearch search, IEnumerable<IColumn> columns, IDictionary<string, object> additionalParameters)
        {
            Draw = draw;
            Start = start;
            Length = length;
            Search = search;
            Columns = columns;
            AdditionalParameters = additionalParameters;
        }
    }
}
