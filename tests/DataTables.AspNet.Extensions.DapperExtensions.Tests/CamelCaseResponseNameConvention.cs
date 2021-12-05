using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.Extensions.DapperExtensions.Tests
{
    /// <summary>
    /// Represents CamelCase response naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class CamelCaseResponseNameConvention : IResponseNameConvention
    {
        public string Draw { get { return "draw"; } }
        public string TotalRecords { get { return "recordsTotal"; } }
        public string TotalRecordsFiltered { get { return "recordsFiltered"; } }
        public string Data { get { return "data"; } }
        public string Error { get { return "error"; } }
    }
}
