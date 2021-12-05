namespace DataTables.AspNet.Core
{
    public interface ISearch
    {
        /// <summary>
        /// Gets the search value.
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        string Value { get; }

        /// <summary>
        /// Gets an indicator whether the search value is a Regular Expression or not.
        /// True if the global filter should be treated as a regular expression for advanced searching, false otherwise.
        /// Note that normally server-side processing scripts will not perform regular expression searching for performance reasons on large data sets, but it is technically possible and at the discretion of your script.
        /// <see href="https://datatables.net/manual/server-side"/>
        /// </summary>
        bool IsRegex { get; }
    }
}
