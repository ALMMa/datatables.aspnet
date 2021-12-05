using DataTables.AspNet.Core;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents a DataTables column.
    /// </summary>
    public class Column : IColumn
    {
        public string Field { get; private set; }
        public string Name { get; private set; }
        public ISearch Search { get; private set; }
        public bool IsSearchable { get; private set; }
        public ISort Sort { get; private set; }
        public bool IsSortable { get; private set; }


        public Column(string name, string field, bool searchable, bool sortable, ISearch search)
        {
            Name = name;
            Field = field;
            IsSortable = sortable;
                        
            IsSearchable = searchable;
            if (!IsSearchable) Search = null;
            else Search = search ?? new Search();
        }


        public bool SetSort(int order, string direction)
        {
            if (!IsSortable) return false;

            Sort = new Sort(order, direction);
            return true;
        }
    }
}
