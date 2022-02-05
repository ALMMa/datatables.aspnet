using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DataTables.AspNet.AspNetCore
{
    /// <summary>
    /// Represents a custom JsonResult that can process IDataTablesResponse accordingly to settings.
    /// </summary>
    public class DataTablesJsonResult<TDataType> : IActionResult
    {
        /// <summary>
        /// Defines the default result content type.
        /// </summary>
        private static readonly string DefaultContentType = "application/json; charset={0}";
        
        /// <summary>
        /// Defines the default result enconding.
        /// </summary>
        private static readonly System.Text.Encoding DefaultContentEncoding = System.Text.Encoding.UTF8;


        private readonly string ContentType;
        private readonly System.Text.Encoding ContentEncoding;
        private readonly IDataTablesResponse<TDataType> Data;


        public DataTablesJsonResult(IDataTablesResponse<TDataType> response)
            : this(response, DefaultContentType, DefaultContentEncoding)
        { }

        public DataTablesJsonResult(IDataTablesResponse<TDataType> response, string contentType, System.Text.Encoding contentEncoding)
        {
            Data = response;
            ContentEncoding = contentEncoding ?? System.Text.Encoding.UTF8;
            ContentType = string.Format(contentType ?? DefaultContentType, contentEncoding.WebName);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = ContentType;

            if (Data != null)
            {
                var content = Data.ToString();
                var contentBytes = ContentEncoding.GetBytes(content);
                await response.Body.WriteAsync(contentBytes, context.HttpContext.RequestAborted);
            }
        }
    }
}
