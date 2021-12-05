namespace DataTables.AspNet.Core.NameConvention
{
    /// <summary>
    /// Define name convention for request parameters.
    /// </summary>
    public interface IRequestNameConvention
    {
        /// <summary>
        /// Gets template for draw.
        /// </summary>
        string Draw { get; }

        /// <summary>
        /// Gets template for data start counter.
        /// </summary>
        string Start { get; }

        /// <summary>
        /// Gets template for data page length.
        /// </summary>
        string Length { get; }

        /// <summary>
        /// Gets template for global search value.
        /// </summary>
        string SearchValue { get; }

        /// <summary>
        /// Gets template for global search regex indicator.
        /// </summary>
        string IsSearchRegex { get; }

        /// <summary>
        /// Gets template for each sort field name.
        /// </summary>
        string SortField { get; }

        /// <summary>
        /// Gets template for each sort direction.
        /// </summary>
        string SortDirection { get; }

        /// <summary>
        /// Gets template for each column field.
        /// </summary>
        string ColumnField { get; }

        /// <summary>
        /// Gets template for each column name.
        /// </summary>
        string ColumnName { get; }

        /// <summary>
        /// Gets template for each column searchable indicator.
        /// </summary>
        string IsColumnSearchable { get; }

        /// <summary>
        /// Gets template for each column sortable indicator.
        /// </summary>
        string IsColumnSortable { get; }

        /// <summary>
        /// Gets template for each column search value.
        /// </summary>
        string ColumnSearchValue { get; }

        /// <summary>
        /// Gets template for each column search regex indicator.
        /// </summary>
        string IsColumnSearchRegex { get; }

        /// <summary>
        /// Gets template for ascending sort.
        /// </summary>
        string SortAscending { get; }

        /// <summary>
        /// Gets template for descending sort.
        /// </summary>
        string SortDescending { get; }
    }
}
