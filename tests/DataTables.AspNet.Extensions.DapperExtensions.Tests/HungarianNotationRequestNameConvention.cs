using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents HungarianNotation request naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class HungarianNotationRequestNameConvention : IRequestNameConvention
    {
        public string ColumnField { get { return "mDataProp_{0}"; } }
        public string ColumnName { get { return "{0}"; } }
        public string IsColumnSortable { get { return "bSortable_{0}"; } }
        public string IsColumnSearchable { get { return "bSearchable_{0}"; } }
        public string IsColumnSearchRegex { get { return "bRegex_{0}"; } }
        public string ColumnSearchValue { get { return "sSearch_{0}"; } }
        public string Draw { get { return "sEcho"; } }
        public string Length { get { return "iDisplayLength"; } }
        public string IsSearchRegex { get { return "bRegex"; } }
        public string SearchValue { get { return "sSearch"; } }
        public string SortDirection { get { return "sSortDir_{0}"; } }
        public string SortField { get { return "sSortCol_{0}"; } }
        public string Start { get { return "iDisplayStart"; } }
        public string SortAscending { get { return "asc"; } }
        public string SortDescending { get { return "desc"; } }
    }
}
