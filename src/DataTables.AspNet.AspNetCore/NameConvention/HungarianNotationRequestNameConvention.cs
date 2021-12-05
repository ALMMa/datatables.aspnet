using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.AspNetCore.NameConvention
{
    /// <summary>
    /// Represents HungarianNotation request naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class HungarianNotationRequestNameConvention : IRequestNameConvention
    {
        public string ColumnField => "mDataProp_{0}";
        public string ColumnName => "{0}";
        public string IsColumnSortable => "bSortable_{0}";
        public string IsColumnSearchable => "bSearchable_{0}";
        public string IsColumnSearchRegex => "bRegex_{0}";
        public string ColumnSearchValue => "sSearch_{0}";
        public string Draw => "sEcho";
        public string Length => "iDisplayLength";
        public string IsSearchRegex => "bRegex";
        public string SearchValue => "sSearch";
        public string SortDirection => "sSortDir_{0}";
        public string SortField => "sSortCol_{0}";
        public string Start => "iDisplayStart";
        public string SortAscending => "asc";
        public string SortDescending => "desc";
    }
}
