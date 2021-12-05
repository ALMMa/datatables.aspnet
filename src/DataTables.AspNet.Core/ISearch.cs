namespace DataTables.AspNet.Core
{
    public interface ISearch
    {
        /// <summary>
        /// Search value.
        /// </summary>
        string Value { get; }
        
        /// <summary>
        /// Flag to indicate if the search term for this column should be treated as regular expression (true) or not (false).
        /// Normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// </summary>
        bool IsRegex { get; }
    }
}
