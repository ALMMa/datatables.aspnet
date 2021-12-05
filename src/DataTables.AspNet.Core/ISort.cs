namespace DataTables.AspNet.Core
{
    public interface ISort
    {
        /// <summary>
        /// Indicates the sort order for composed (multi) sorting.
        /// </summary>
        int Order { get; }
        /// <summary>
        /// Ordering direction for this column.
        /// It will be 'Ascending' or 'Descending' to indicate ordering direction.
        /// </summary>
        SortDirection Direction { get; }
    }
}
