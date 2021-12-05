namespace DataTables.AspNet.Core.NameConvention
{
    /// <summary>
    /// Define name convention for response parameters.
    /// </summary>
    public interface IResponseNameConvention
    {
        /// <summary>
        /// Gets template for draw.
        /// </summary>
        string Draw { get; }
        /// <summary>
        /// Gets template for total recordd counter.
        /// </summary>
        string TotalRecords { get; }
        /// <summary>
        /// Gets template for total filtered records counter.
        /// </summary>
        string TotalRecordsFiltered { get; }
        /// <summary>
        /// Gets template for data.
        /// </summary>
        string Data { get; }
        /// <summary>
        /// Gets template for error.
        /// </summary>
        string Error { get; }
    }
}
