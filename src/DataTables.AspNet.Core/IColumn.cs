namespace DataTables.AspNet.Core
{
    /// <summary>
    /// Defines DataTables column members.
    /// </summary>
    public interface IColumn
    {
        /// <summary>
        /// Gets column name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets column field.
        /// </summary>
        string Field { get; }
        /// <summary>
        /// Gets column searchable indicator.
        /// </summary>
        bool IsSearchable { get; }
        /// <summary>
        /// Gets column search definition or null if column is not searchable.
        /// </summary>
        ISearch Search { get; }
        /// <summary>
        /// Gets column sortable indicator.
        /// </summary>
        bool IsSortable { get; }
        /// <summary>
        /// Gets column sort definition or null if column is not sortable.
        /// </summary>
        ISort Sort { get; }



        /// <summary>
        /// Sets column sort definition.
        /// </summary>
        /// <param name="order">Sort order.</param>
        /// <param name="direction">Sort direction.</param>
        /// <returns>True if sort could be set, False otherwise.</returns>
        bool SetSort(int order, string direction);
    }
}
