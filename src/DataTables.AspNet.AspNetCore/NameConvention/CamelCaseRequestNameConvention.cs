using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.AspNetCore.NameConvention
{
    /// <summary>
    /// Represents CamelCase request naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class CamelCaseRequestNameConvention : IRequestNameConvention
    {
        public string Draw => "draw";
        public string Start => "start";
        public string Length => "length";
        public string SearchValue => "search[value]";
        public string IsSearchRegex => "search[regex]";
        public string SortField => "order[{0}][column]";
        public string SortDirection => "order[{0}][dir]";
        public string ColumnField => "columns[{0}][data]";
        public string ColumnName => "columns[{0}][name]";
        public string IsColumnSearchable => "columns[{0}][searchable]";
        public string IsColumnSortable => "columns[{0}][orderable]";
        public string ColumnSearchValue => "columns[{0}][search][value]";
        public string IsColumnSearchRegex => "columns[{0}][search][regex]";
        public string SortAscending => "asc";
        public string SortDescending => "desc";
    }
}
