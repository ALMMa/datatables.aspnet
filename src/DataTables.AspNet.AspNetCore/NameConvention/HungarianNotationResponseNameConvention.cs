using DataTables.AspNet.Core.NameConvention;

namespace DataTables.AspNet.AspNetCore.NameConvention
{
    /// <summary>
    /// Represents HungarianNotation response naming convention for DataTables.AspNet.AspNetCore.
    /// </summary>
    public class HungarianNotationResponseNameConvention : IResponseNameConvention
    {
        public string Draw => "sEcho";
        public string TotalRecords => "iTotalRecords";
        public string TotalRecordsFiltered => "iTotalDisplayRecords";
        public string Data => "aaData";
        public string Error => string.Empty;
        public string AdditionalParameters => string.Empty;
    }
}
