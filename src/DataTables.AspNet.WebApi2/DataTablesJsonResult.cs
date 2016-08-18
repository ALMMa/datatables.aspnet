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
using DataTables.AspNet.Core;
using System.Web.Http.Results;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace DataTables.AspNet.WebApi2
{
    /// <summary>
    /// Represents a custom JsonResult that can process IDataTablesResponse accordingly to settings.
    /// </summary>
    public class DataTablesJsonResult : JsonResult<IDataTablesResponse>
    {
		/// <summary>
		/// Defines the default json settings.
		/// </summary>
		private static readonly Newtonsoft.Json.JsonSerializerSettings DefaultJsonSettings = new Newtonsoft.Json.JsonSerializerSettings() { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() };
		/// <summary>
		/// Defines the default result enconding.
		/// </summary>
		private static readonly System.Text.Encoding DefaultContentEncoding = System.Text.Encoding.UTF8;



		public DataTablesJsonResult(IDataTablesResponse response, HttpRequestMessage requestMessage)
			: this(response, DefaultContentEncoding, requestMessage)
		{ }

		public DataTablesJsonResult(IDataTablesResponse response, Encoding contentEncoding, HttpRequestMessage requestMessage)
			: base(response, DefaultJsonSettings, contentEncoding, requestMessage)
		{
		}

		public override Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew<HttpResponseMessage>(() =>
			{
				if (cancellationToken.IsCancellationRequested) return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "Request was cancelled");

				var response = Request.CreateResponse();

				if (cancellationToken.IsCancellationRequested) return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "Request was cancelled");

				var data = Encoding.GetBytes(Content.ToString());

				if (cancellationToken.IsCancellationRequested) return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "Request was cancelled");

				var stream = new MemoryStream();
				stream.Write(data, 0, data.Length);
				stream.Seek(0, SeekOrigin.Begin); // Forces the stream to seek to it's origin so available data is written into output (response).

				if (cancellationToken.IsCancellationRequested) return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "Request was cancelled");

				response.Content = new StreamContent(stream);
				response.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(String.Format("application/json; charset={0}", Encoding.WebName));
				response.Content.Headers.ContentEncoding.Add(Encoding.WebName);

				if (cancellationToken.IsCancellationRequested) return Request.CreateErrorResponse(System.Net.HttpStatusCode.NoContent, "Request was cancelled");
				else return response;
			});
		}
	}
}
