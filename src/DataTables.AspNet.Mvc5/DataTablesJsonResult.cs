#region Copyright
/* The MIT License (MIT)

Copyright (c) 2014 Anderson Luiz Mendes Matos (Brazil)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion Copyright

using System;
using System.Web.Mvc;
using DataTables.AspNet.Core;

namespace DataTables.AspNet.Mvc5
{
    /// <summary>
    /// Represents a custom JsonResult that can process IDataTablesResponse accordingly to settings.
    /// </summary>
    public class DataTablesJsonResult : System.Web.Mvc.JsonResult
    {
        /// <summary>
        /// Defines the default result content type.
        /// </summary>
        private static readonly string DefaultContentType = "application/json; charset={0}";
        /// <summary>
        /// Defines the default result enconding.
        /// </summary>
        private static readonly System.Text.Encoding DefaultContentEncoding = System.Text.Encoding.UTF8;
        /// <summary>
        /// Defines the default json request behavior.
        /// </summary>
        private static readonly JsonRequestBehavior DefaultRequestBehavior = JsonRequestBehavior.DenyGet;


        public DataTablesJsonResult(IDataTablesResponse response)
            : this(response, DefaultContentType, DefaultContentEncoding, DefaultRequestBehavior)
        { }

        public DataTablesJsonResult(IDataTablesResponse response, JsonRequestBehavior jsonRequestBehavior)
            : this(response, DefaultContentType, DefaultContentEncoding, jsonRequestBehavior)
        { }

        public DataTablesJsonResult(IDataTablesResponse response, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior jsonRequestBehavior)
        {
            Data = response;
            ContentEncoding = contentEncoding ?? System.Text.Encoding.UTF8;
            ContentType = String.Format(contentType ?? DefaultContentType, ContentEncoding.WebName);
            JsonRequestBehavior = jsonRequestBehavior;
        }

        /// <summary>
        /// Executes and writes result content into response.
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet && context.HttpContext.Request.HttpMethod.ToUpperInvariant().Equals("GET"))
                throw new NotSupportedException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");

            var response = context.HttpContext.Response;
            
            response.ContentType = ContentType;
            response.ContentEncoding = ContentEncoding;

            if (Data != null)
            {
                var content = Data.ToString();
                response.Write(content);
            }
        }
    }
}
