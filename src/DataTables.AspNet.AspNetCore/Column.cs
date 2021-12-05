using DataTables.AspNet.Core;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// Represents a DataTables column.
    /// </summary>
    public class Column : IColumn
    {
        public string Field { get; }
        public string Name { get; }
        public ISearch Search { get; }
        public bool IsSearchable { get; }
        public ISort Sort { get; private set; }
        public bool IsSortable { get; }


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
